using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogWebApi.Model;

namespace LogWebApi.Service
{
    public class RequestMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ITraceHelper traceHelper)
        {
            // read request content
            string requestContent = "";       

            context.Request.EnableBuffering();
            
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                bufferSize: 1024,
                leaveOpen: true))
            {
                requestContent = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                SysOpenTelemetryCollector.ExportTelemetryTask(traceHelper);
            }
        }
    }
}
