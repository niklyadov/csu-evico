using System.ComponentModel.DataAnnotations;

namespace Evico.Api.Attributes;

public class AllowedContentTypeAttribute : ValidationAttribute
{
    private readonly string[] _contentTypes;

    public AllowedContentTypeAttribute(string[] contentTypes)
    {
        _contentTypes = contentTypes;
    }

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Input file is null");

        if (value is IFormFile file)
            if (!_contentTypes.Contains(file.ContentType))
                return new ValidationResult(GetErrorMessage());

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return "Photo content type is not allowed!";
    }
}