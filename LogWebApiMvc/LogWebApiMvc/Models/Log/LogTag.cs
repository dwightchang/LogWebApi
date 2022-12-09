using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Newtonsoft.Json;

namespace LogWebApiMvc.Models.Log
{
    public class LogTag
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Req { get; set; }
    }
}