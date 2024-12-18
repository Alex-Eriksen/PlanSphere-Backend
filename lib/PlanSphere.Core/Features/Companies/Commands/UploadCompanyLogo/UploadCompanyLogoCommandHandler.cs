﻿using MediatR;
using Microsoft.Extensions.Logging;
using PlanSphere.Core.Attributes;
using PlanSphere.Core.Clients.FileStorage;
using PlanSphere.Core.Enums;
using PlanSphere.Core.Interfaces;
using PlanSphere.Core.Interfaces.Repositories;
using PlanSphere.Core.Utilities.Builder;

namespace PlanSphere.Core.Features.Companies.Commands.UploadCompanyLogo;

[HandlerType(HandlerType.SystemApi)]
public class UploadCompanyLogoCommandHandler(
    ICompanyRepository companyRepository,
    IPrivateAzureBlobStorage privateAzureBlobStorage,
    ILogger<UploadCompanyLogoCommandHandler> logger
) : IRequestHandler<UploadCompanyLogoCommand, string>
{
    private readonly ICompanyRepository _companyRepository = companyRepository ?? throw new ArgumentNullException(nameof(companyRepository));
    private readonly ILogger<UploadCompanyLogoCommandHandler> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    private readonly IPrivateAzureBlobStorage _privateAzureBlobStorage = privateAzureBlobStorage ?? throw new ArgumentNullException(nameof(privateAzureBlobStorage));
    
    public async Task<string> Handle(UploadCompanyLogoCommand request, CancellationToken cancellationToken)
    {
        _logger.BeginScope("Uploading company logo for company with id: [{companyId}]", request.CompanyId);
        
        _logger.LogInformation("Generating path and trying to upload image to directory");
        var pathBuilder = new PathBuilder();
        var filePath = pathBuilder
            .AddOrganisationDirectory(request.OrganisationId)
            .AddCompanyDirectory(request.CompanyId)
            .AddLogo(request.Image.FileName)
            .GeneratePath();

        using (var ms = new MemoryStream())
        {
            request.Image.CopyTo(ms);
            var fileBytes = ms.ToArray();
            await _privateAzureBlobStorage.SaveAsync(filePath, fileBytes);
            _logger.LogInformation("Successfully saved file to directory: {filepath}", filePath);
        }
        _logger.LogInformation("Saving logo file path on company with id: [{companyId}]", request.CompanyId);
        await _companyRepository.UploadLogoAsync(request.CompanyId, filePath, cancellationToken);
        _logger.LogInformation("Saved logo file path on company with id: [{companyId}]", request.CompanyId);

        return filePath;
    }
}