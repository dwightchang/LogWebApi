using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProxy.Models.Order
{
    public class FindOrderResult
    {
        public int Sn { get; set; }
        public int CustomerNo { get; set; }
        public int ProductNo { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
