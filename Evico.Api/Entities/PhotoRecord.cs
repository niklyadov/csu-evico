using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Evico.Api.Entities;

[NotMapped]
public record PhotoRecord : EntityRecord
{
    public long? AuthorId { get; set; }
    [JsonIgnore]
    public ProfileRecord? Author { get; set; }
    public String? Comment { get; set; }
    public Uri Uri { get; set; } = default!;
    [JsonIgnore]
    public MinioBucketNames MinioBucket { get; set; }
    [JsonIgnore]
    public Guid MinioInternalId { get; set; }
}