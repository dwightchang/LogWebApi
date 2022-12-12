using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace LogWebApiMvc.Models.Log
{
    public class RequestLogData
    {
        public Stopwatch Watch { get; set; }
        public string RequestData { get; set; }
        public string UrlPath { get; set; }
    }
}