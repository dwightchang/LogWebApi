using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Model.Entity
{
    public class Order
    {
        public int Sn { get; set; }
        public int CustomerNo { get; set; }
        public int ProductNo { get; set; }
        public int Quantity { get; set; }
        public string CreatedTime { get; set; }
    }
}
