using System.ComponentModel.DataAnnotations;

namespace Evico.Api.Attributes;

public class MaxFileSizeAttribute : ValidationAttribute
{
    private readonly int _maxFileSize;

    public MaxFileSizeAttribute(int maxFileSize)
    {
        _maxFileSize = maxFileSize;
    }

    protected override ValidationResult? IsValid(
        object? value, ValidationContext validationContext)
    {
        if (value is null) return new ValidationResult("Input file is null");

        if (value is IFormFile file)
            if (file.Length > _maxFileSize)
                return new ValidationResult(GetErrorMessage());

        return ValidationResult.Success;
    }

    private string GetErrorMessage()
    {
        return $"Maximum allowed file size is {_maxFileSize} bytes.";
    }
}