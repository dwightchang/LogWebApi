﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class TraceContent
    {
        public string TraceId { get; set; } = Guid.NewGuid().ToString("N");
    }
}
