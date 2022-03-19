using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using TaxCalculator.Models.Models;

namespace TaxCalculator.Extensions
{

    public static class ExceptionMiddlewareExtensions
    {
        //Global built-in error handling
        //All errors are catched here. We save the actual error to our logger and return a generic error
        public static void CongifureExceptionHandler(this IApplicationBuilder app, ILogger logger)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        //Log error
                        logger.LogError($"Something went wrong: {contextFeature.Error}");
                        //Display Error JSON to the client
                        await context.Response.WriteAsync(new ErrorDetails
                        {
                            StatusCode = context.Response.StatusCode,
                            Message = "Internal Server Error from the built-in middleware"
                        }.ToString());
                    }
                });
            });
        }
    }
}
