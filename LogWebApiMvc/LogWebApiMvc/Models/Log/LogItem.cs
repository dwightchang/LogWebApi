using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LogWebApiMvc.Models.Log
{
    public class LogItem
    {
        public string LogTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        public string Service { get; set; } = "";
        public string Level { get; set; }
        public string Message { get; set; }
        public string Application { get; set; } = "Order";
        public string Type { get; set; }= "Message";
        public long Duration { get; set; } = 0;
        public string TraceId { get; set; }
        public LogTag Tag { get; set; }
    }
}