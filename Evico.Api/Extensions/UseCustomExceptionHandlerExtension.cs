using Evico.Api.Middlewares;

namespace Evico.Api.Extensions;

public static class UseCustomExceptionHandlerExtension
{
    public static void UseCustomExceptionHandler(this WebApplication application)
    {
        application.UseMiddleware<ExceptionHandlerMiddleware>();
    }
}