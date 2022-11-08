using System.ComponentModel.DataAnnotations.Schema;

namespace Evico.Api.Entities;

[NotMapped]
public abstract record CategoryRecord : EntityRecord
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public CategoryRecord? Parent { get; set; } = null;
}