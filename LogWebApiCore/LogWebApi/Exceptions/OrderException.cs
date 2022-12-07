using LogWebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Exceptions
{
    public class OrderException : Exception
    {
        public virtual ExceptionSeverity Severity { get; set; } = ExceptionSeverity.Error;
        public string ErrorMessage { get; set; }
    }
}
