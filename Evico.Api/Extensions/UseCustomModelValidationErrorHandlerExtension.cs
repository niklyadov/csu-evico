using FluentResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Evico.Api.Extensions;

public static class UseCustomModelValidationErrorHandlerExtension
{
    public static void UseCustomModelValidationErrorHandler(this IServiceCollection services)
    {
        services.Configure<ApiBehaviorOptions>(o =>
        {
            o.InvalidModelStateResponseFactory = actionContext =>
            {
                Result? result = null;

                foreach (var modelStateKeyValue in actionContext.ModelState)
                {
                    var modelState = modelStateKeyValue.Value;
            
                    if (modelState.ValidationState == ModelValidationState.Valid)
                        continue;

                    var mainValidateError = new Error("One or more errors occurred when validate model");
                    mainValidateError.Metadata.Add("ValidationState", modelState.ValidationState.ToString());
            
                    result = Result.Fail(mainValidateError
                        .CausedBy(modelState.Errors.Select(MapError))
                    );
                }
        
                return new BadRequestObjectResult(result?.GetReport());
            };
        });
    }

    private static IError MapError(ModelError modelError)
    {
        var error = new Error(modelError.ErrorMessage);

        if (modelError.Exception != null)
            error.CausedBy(modelError.Exception);

        return error;
    }
}