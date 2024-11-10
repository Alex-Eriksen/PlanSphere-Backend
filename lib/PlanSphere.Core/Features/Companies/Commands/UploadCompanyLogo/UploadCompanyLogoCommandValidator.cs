using FluentValidation;
using Microsoft.AspNetCore.Http;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Features.Companies.Commands.UploadCompanyLogo;

public class UploadCompanyLogoCommandValidator : AbstractValidator<UploadCompanyLogoCommand>
{
    public UploadCompanyLogoCommandValidator()
    {
        RuleFor(x => x.OrganisationId).NotNull();
        RuleFor(x => x.CompanyId).NotNull();
        RuleFor(x => x.Image)
            .Must(BeAValidImage)
            .WithMessage("Invalid file. Only .jpg and .png files are allowed, and the file size must not exceed 5MB.");
    }
    
    private bool BeAValidImage(IFormFile? file)
    {
        if (file == null)
        {
            return true;
        }

        if (file.Length > ByteSizeConstants.FIVE_MEGABYTES)
        {
            return false;
        }

        var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(ext) || !PermittedImageExtensions.All.Contains(ext))
        {
            return false;
        }

        return true;
    }
}