using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProxy.Models.Order
{
    public class FindOrderResp
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public FindOrderResult Result { get; set; }
    }
}
