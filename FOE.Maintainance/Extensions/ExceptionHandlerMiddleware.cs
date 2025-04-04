﻿using Contracts.Base;
using Microsoft.AspNetCore.Diagnostics;
using Core.Entities.ErrorModel;
using Core.Exceptions;
namespace FOE.Maintainance.Extensions
{
    public static class ExceptionHandlerMiddleware
    {

        public static void HandleExceptions(this WebApplication app)
            => app.ExceptionsMiddleware(app.Services.GetRequiredService<ILoggerManager>());

         static void ExceptionsMiddleware(this WebApplication app,ILoggerManager logger)
            => app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    var errorOccured = context.Features.Get<IExceptionHandlerFeature>();
                    if(errorOccured!=null)
                    {
                        logger.LogError(errorOccured.Error.Message);
                        var statuscode = errorOccured.Error switch
                        {
                            NotFoundException => StatusCodes.Status404NotFound,
                            BadRequestException=> StatusCodes.Status400BadRequest,
                            _ => StatusCodes.Status500InternalServerError
                        };
                        context.Response.StatusCode = statuscode;
                       await   context.Response.WriteAsJsonAsync(new Error(statuscode,errorOccured.Error.Message));
                    }
                });
            });
    } 
}
