using LogWebApi.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Exceptions
{
    public class InvalidProductNoException : OrderException
    {
        public override ExceptionSeverity Severity => ExceptionSeverity.Warn;

        public InvalidProductNoException(int productNo)
        {
            ErrorMessage = $"Invalid product no : {productNo}";
        }
    }
}
