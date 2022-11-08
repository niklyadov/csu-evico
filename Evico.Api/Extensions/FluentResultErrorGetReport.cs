using FluentResults;
using Newtonsoft.Json;

namespace Evico.Api.Extensions;

public static class FluentResultErrorGetReport
{
    public static string GetReport<T>(this Result<T> result, bool formatted = true)
    {
        result.Log();
        
        return result.GetReport(formatted, typeof(T));
    }

    public static string GetReport(this Result result, bool formatted = true)
    {
        result.Log();
        
        return result.GetReport(formatted, null);
    }

    public static string GetReport(this ResultBase result, bool formatted = true, Type? type = null)
    {
        var report = new ErrorReport
        {
            ResultType = type,
            Errors = result.Errors
            //Reasons = result.Reasons
        };

        return JsonConvert.SerializeObject(report, 
            formatted ? Formatting.Indented : Formatting.None);
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
    public Type? ResultType { get; set; }
}


public class ReportException : Exception
{
    public ReportException(string message) : base(message)
    {
    }
}