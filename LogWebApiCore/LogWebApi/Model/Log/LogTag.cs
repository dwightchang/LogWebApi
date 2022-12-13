using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace LogWebApi.Model.Log
{
    public class LogTag
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Req { get; set; }
    }
}
