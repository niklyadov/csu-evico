using FluentResults;
using Newtonsoft.Json;

namespace Evico.Api.Extensions;

public static class FluentResultErrorGetReport
{
    public static string GetReport<T>(this Result<T> result, bool formatted = true)
    {
        var report = new ErrorReportTyped<T?>
        {
            Errors = result.Errors,
            Reasons = result.Reasons,
            ResultType = typeof(T),
            ResultValue = result.ValueOrDefault
        };

        return JsonConvert.SerializeObject(report, formatted ? Formatting.Indented : Formatting.None);
    }
    
    public static string GetReport(this Result result, bool formatted = true)
    {
        var report = new ErrorReport
        {
            Errors = result.Errors,
            Reasons = result.Reasons
        };

        return JsonConvert.SerializeObject(report, formatted ? Formatting.Indented : Formatting.None);
    }

    public static ReportException GetReportException<T>(this Result<T> result)
    {
        return new ReportException(GetReport(result, false).Replace('"', '\''));
    }
}

internal record ErrorReport
{
    public List<IError> Errors { get; set; } = new();
    public List<IReason> Reasons { get; set; } = new();
    public DateTime GeneratedAt { get; set; } = DateTime.Now;
    public DateTime GeneratedAtUtc { get; set; } = DateTime.UtcNow;
}

internal record ErrorReportTyped<T> : ErrorReport
{
    public Type? ResultType { get; set; }
    public T? ResultValue { get; set; }
}

public class ReportException : Exception
{
    public ReportException(String message): base(message)
    {
    }
}