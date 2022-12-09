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
        public static ThreadLocal<TraceContent> Trace;

        public static void Init()
        {
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

            if (string.IsNullOrEmpty(Trace.Value.TraceId))
            {
                Trace.Value.TraceId = HttpContext.Current.Request.Headers["TraceId"];
            }

            if (string.IsNullOrEmpty(Trace.Value.TraceId))
            {
                Trace.Value.TraceId = Guid.NewGuid().ToString("N");
            }

            return Trace.Value;
        }
    }
}