using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class TraceFactory : ITraceFactory
    {
        public static AsyncLocal<TraceContext> Context = new AsyncLocal<TraceContext>()
        {
            Value = new TraceContext()
        };
    }
}
