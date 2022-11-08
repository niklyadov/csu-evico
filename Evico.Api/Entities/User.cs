using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Api.Entities;

[NotMapped]
public record User : EntityRecord
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}