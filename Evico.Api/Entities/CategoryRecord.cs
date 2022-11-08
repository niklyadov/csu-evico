using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

[NotMapped]
public abstract record CategoryRecord : EntityRecord
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public long? ParentId { get; set; }
    [JsonIgnore]
    public CategoryRecord? Parent { get; set; }
}