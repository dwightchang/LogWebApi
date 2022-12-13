using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace LogWebApi.Model
{
    public class TraceHelper : ITraceHelper
    {
        public static AsyncLocal<TraceContent> Trace = new AsyncLocal<TraceContent>()
        {
            Value = new TraceContent()
        };

        public TraceContent GetTrace()
        {
            if (Trace == null)
            {
                Trace = new AsyncLocal<TraceContent>();
            }

            if (Trace.Value == null)
            {
                Trace.Value = new TraceContent();
            }

            if (string.IsNullOrEmpty(Trace.Value.TraceId))
            {
                Trace.Value.TraceId = Guid.NewGuid().ToString("N");
            }

            return Trace.Value;
        }
    }
}
