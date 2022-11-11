using System.Text.Json.Serialization;

namespace Evico.Api;

public class MinioBucketsConfiguration
{
    public List<MinioBucketConfiguration> Buckets { get; set; } = default!;

    public MinioBucketConfiguration GetBucket(MinioBucketNames bucketName)
    {
        return Buckets.Single(b => b.Name == bucketName);
    }
}

public class MinioBucketConfiguration
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public MinioBucketNames Name { get; set; }
    public String Location { get; set; } = String.Empty;
    public String NameString => Name.ToString().ToLower();
}

public enum MinioBucketNames
{
    UserAvatars,
    PlacePhotos,
    EventPhotos,
    ReviewPhotos
}