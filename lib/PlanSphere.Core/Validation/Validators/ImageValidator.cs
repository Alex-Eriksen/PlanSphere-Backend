using FluentValidation;
using Microsoft.AspNetCore.Http;
using PlanSphere.Core.Constants;

namespace PlanSphere.Core.Validation.Validators;

public class ImageValidator : AbstractValidator<IFormFile>
{
    public ImageValidator()
    {
        RuleFor(x => x)
            .Must(BeAValidImage)
            .WithMessage("Invalid file. Only .jpg and .png files are allowed, and the file size must not exceed 5MB.");
    }
    
    private static bool BeAValidImage(IFormFile? file)
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
        return !string.IsNullOrEmpty(ext) && PermittedImageExtensions.All.Contains(ext);
    }
}