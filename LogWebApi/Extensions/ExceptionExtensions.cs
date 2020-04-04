using LogWebApi.Exceptions;
using LogWebApi.Model;
using LogWebApi.Service;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace LogWebApi.Extensions
{
    public static class ExceptionExtensions
    {
        public static void ConfigureExceptionHandler(this IApplicationBuilder app)
        {
            app.UseExceptionHandler(appError =>
            {
                appError.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.OK;
                    context.Response.ContentType = "application/json";

                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
                    if (contextFeature != null)
                    {
                        var orderException = contextFeature.Error as OrderException;

                        string json = "";
                        string errorMessage = "";

                        if (orderException != null) // customized exception, writing log according to its defined severity
                        {
                            errorMessage = orderException.ErrorMessage;                           
                            SysLogger.Log(orderException.ToString(), orderException.Severity);
                        }
                        else
                        {
                            errorMessage = contextFeature.Error.Message;
                            SysLogger.Error(contextFeature.Error.ToString());
                        }

                        json = Newtonsoft.Json.JsonConvert.SerializeObject(new ResponseViewModel<object>
                        {
                            Success = false,
                            Message = errorMessage,
                            Result = null
                        });

                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}
