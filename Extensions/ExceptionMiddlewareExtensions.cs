
using HackerNews.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System.Net;
using HackerNews.LoggerService;
namespace HackerNews.Extensions.GlobalErrorHandling
{
  public static class ExceptionMiddlewareExtensions
        {
            public static void ConfigureExceptionHandler(this IApplicationBuilder app, ILoggerManager logger)
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
                            logger.LogError($"Error while getting News: {contextFeature.Error}");

                            await context.Response.WriteAsync(new ErrorDetails()
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Internal Server Error."
                            }.ToString());
                        }
                    });
                });
            }
        }
    
}
