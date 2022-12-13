using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogWebApi.Model.Log
{
    public class LogItem
    {
        public string LogTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss:fff");
        public string Service { get; set; } = "";
        public string Level { get; set; }
        public string Message { get; set; }
        public string Application { get; set; } = "Order";
        public string Type { get; set; } = "Message";
        public long Duration { get; set; } = 0;
        public string TraceId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SpanId { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public DateTime? StartTime { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string SourceName { get; set; }
        public string ActivityName { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Url { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Clientip { get; set; }
        public LogTag Tag { get; set; }
    }
}
