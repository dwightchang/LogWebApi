using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class TraceHelper : ITraceHelper
    {
        public static AsyncLocal<TraceContent> Context = new AsyncLocal<TraceContent>()
        {
            Value = new TraceContent()
        };
    }
}
