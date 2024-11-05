using MediatR;
using Microsoft.AspNetCore.Http;

namespace PlanSphere.Core.Features.Companies.Commands.UploadCompanyLogo;

public record UploadCompanyLogoCommand(IFormFile Image) : IRequest<string>
{
    public ulong OrganisationId { get; set; }
    public ulong CompanyId { get; set; }
}