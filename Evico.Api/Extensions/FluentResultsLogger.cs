using FluentResults;

namespace Evico.Api.Extensions;

public class FluentResultsLogger : IResultLogger
{
    private readonly ILogger _logger;

    public FluentResultsLogger(ILogger logger)
    {
        _logger = logger;
    }

    public void Log(string context, string content, ResultBase result, LogLevel logLevel)
    {
        if (result.IsSuccess)
        {
            _logger.LogDebug("Success Result: {Reasons}, Content: {Content} <{Context}>",
                result.GetReport(), content, context);
            return;
        }

        _logger.LogError("Result: {Reasons}, Content: {Content} <{Context}>",
            result.GetReport(), content, context);
    }

    public void Log<TContext>(string content, ResultBase result, LogLevel logLevel)
    {
        if (result.IsSuccess)
        {
            _logger.LogDebug("Success Result: {Reasons}, Content: {Content} <{Context}>",
                result.GetReport(), content, typeof(TContext).FullName);
            return;
        }

        _logger.LogError("Result: {Reasons}, Content: {Content} <{Context}>",
            result.GetReport(), content, typeof(TContext).FullName);
    }
}