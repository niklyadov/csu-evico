using FluentResults;
using Newtonsoft.Json;

namespace Evico.Api.Extensions;

public static class FluentResultErrorGetReport
{
    public static String GetReport<T>(this Result<T> result)
    {
        var report = new ErrorReport<T?>
        {
            Errors = result.Errors,
            Reasons = result.Reasons,
            ResultType = typeof(T),
            ResultValue = result.ValueOrDefault
        };
        
        return JsonConvert.SerializeObject(report, Formatting.Indented);
    }
}

internal record ErrorReport<T>
{
    public List<IError> Errors { get; set; } = new();
    public List<IReason> Reasons { get; set; } = new();
    public Type? ResultType { get; set; }
    public T? ResultValue { get; set; }
    public DateTime GeneratedAt { get; set; } = DateTime.Now;
    public DateTime GeneratedAtUtc { get; set; } = DateTime.UtcNow;
}