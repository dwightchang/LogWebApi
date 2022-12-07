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

        public async Task Invoke(HttpContext context, ITraceFactory traceFactory)
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

            // setup response body listener
            string responseContent;
            var originalBodyStream = context.Response.Body;

            Stopwatch watch = Stopwatch.StartNew();

            using (var myResponseBody = new MemoryStream())
            {
                context.Response.Body = myResponseBody;

                await _next(context);
                watch.Stop();

                myResponseBody.Seek(0, SeekOrigin.Begin);
                using (var reader = new StreamReader(myResponseBody))
                {
                    responseContent = await reader.ReadToEndAsync();
                    myResponseBody.Seek(0, SeekOrigin.Begin);

                    await myResponseBody.CopyToAsync(originalBodyStream);
                }
            }                                              

            decimal seconds = ((decimal)watch.ElapsedMilliseconds) / 1000m;
            SysLogger.RequestElapsedTime.Info($"{context.Request.Path.Value} {seconds.ToString("0.#")} {requestContent} {responseContent}");
        }
    }
}
