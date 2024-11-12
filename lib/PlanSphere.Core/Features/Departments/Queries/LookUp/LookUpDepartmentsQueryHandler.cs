using AutoMapper;
using Domain.Entities.EmbeddedEntities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Features.Departments.DTOs;
using PlanSphere.Core.Interfaces.Repositories;

namespace PlanSphere.Core.Features.Departments.Queries.LookUp;

[HandlerType(HandlerType.SystemApi)]
public class LookUpDepartmentsQueryHandler(
    ILogger<LookUpDepartmentsQueryHandler> logger,
    IUserRepository userRepository,
    IDepartmentRepository departmentRepository,
    IMapper mapper
) : IRequestHandler<LookUpDepartmentsQuery, List<DepartmentLookUpDTO>>
{
    private readonly ILogger<LookUpDepartmentsQueryHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    private readonly IDepartmentRepository _departmentRepository = departmentRepository ?? throw new ArgumentNullException(nameof(departmentRepository));
    private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

    public async Task<List<DepartmentLookUpDTO>> Handle(LookUpDepartmentsQuery request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Looking up departments.");
        _logger.LogInformation("Retrieving departments that the user with id: [{userId}] has access to.", request.UserId);
        var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
        var userRoles = user.Roles.Select(x => x.Role);

        var enumerable = userRoles.ToList();
        var departmentIds = enumerable
            .SelectMany(x => x.OrganisationRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Organisation)
            .SelectMany(x => x.Companies)
            .SelectMany(x => x.Departments)
            .Select(x => x.Id)
            .ToList();
        
        departmentIds.AddRange(enumerable
            .SelectMany(x => x.CompanyRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Company)
            .SelectMany(x => x.Departments)
            .Select(x => x.Id)
            .ToList()
        );
        
        departmentIds.AddRange(enumerable
            .SelectMany(x => x.DepartmentRoleRights)
            .Where(x => x.Right.AsEnum <= Right.View)
            .Select(x => x.Department.Id)
            .ToList()
        );
        departmentIds = departmentIds.Distinct().ToList();
        var departments = await _departmentRepository.GetQueryable().Where(x => departmentIds.Contains(x.Id)).AsSplitQuery().ToListAsync(cancellationToken);
        _logger.LogInformation("Retrieved departments that the user with id: [{userId}] has access to.", request.UserId);
        
        var departmentLookUpDtos = _mapper.Map<List<DepartmentLookUpDTO>>(departments);

        return departmentLookUpDtos;
    }
}