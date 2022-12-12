using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Web;

namespace LogWebApiMvc.Models
{
    public class TraceHelper
    {
        public static ThreadLocal<TraceContent> Trace = new ThreadLocal<TraceContent>();

        public static void Init()
        {
            Trace = new ThreadLocal<TraceContent>();
            GetTrace();
        }

        public static TraceContent GetTrace()
        {
            if (Trace == null)
            {
                Trace = new ThreadLocal<TraceContent>();
            }

            if (Trace.Value == null)
            {
                Trace.Value = new TraceContent();
            }

            if (HttpContext.Current != null &&
                HttpContext.Current.Request != null &&
                HttpContext.Current.Request.Headers != null &&
                HttpContext.Current.Request.Headers.Count > 0)
            {
                var headerTraceId = HttpContext.Current.Request.Headers["TraceId"];

                if (!string.IsNullOrEmpty(headerTraceId))
                {
                    Trace.Value.TraceId = headerTraceId;
                }
            }

            if (string.IsNullOrEmpty(Trace.Value.TraceId))
            {
                Trace.Value.TraceId = Guid.NewGuid().ToString("N");
            }

            return Trace.Value;
        }
    }
}