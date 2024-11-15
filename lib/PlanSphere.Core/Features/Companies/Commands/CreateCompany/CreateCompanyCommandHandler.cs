﻿using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces.Repositories;
using Right = Domain.Entities.EmbeddedEntities.Right;

namespace PlanSphere.Core.Features.Companies.Commands.CreateCompany;

[HandlerType(HandlerType.SystemApi)]
public class CreateCompanyCommandHandler(
    ICompanyRepository companyRepository,
    IOrganisationRepository organisationRepository,
    ILogger<CreateCompanyCommandHandler> logger,
    IMapper mapper
) : IRequestHandler<CreateCompanyCommand>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly IOrganisationRepository _organisationRepository = organisationRepository ?? throw new ArgumentNullException(nameof(organisationRepository));
    private readonly ILogger<CreateCompanyCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task Handle(CreateCompanyCommand command, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Creating company");
        _logger.LogInformation("Creating company on organisation with id: [{organisationId}]", command.OrganisationId);
        var company = _mapper.Map<Company>(command);

        var organisation = await _organisationRepository.GetByIdAsync(command.OrganisationId, cancellationToken);

        if (command.Request.InheritAddress)
        {
            company.Address.ParentId = organisation.AddressId;
        }

        company.Settings = new CompanySettings
        {
            DefaultWorkSchedule = new WorkSchedule
            {
                IsDefaultWorkSchedule = true
            },
            DefaultRole = new Role
            {
                Name = company.Name + "-default-role",
                CompanyRoleRights =
                [
                    new CompanyRoleRight
                    {
                        RightId = (ulong) Right.View,
                        Company = company
                    }
                ],
                CompanyRole = new CompanyRole
                {
                    Company = company
                }
            }
        };

        var createdCompany = await _companyRepository.CreateAsync(company, cancellationToken);
        _logger.LogInformation("Created company with new id: [{companyId}] on organisation with id: [{organisationId}]", command.OrganisationId, createdCompany.Id);
    }
}