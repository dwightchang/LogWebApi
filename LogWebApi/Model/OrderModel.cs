using LogWebApi.Model.Entity;
using LogWebApi.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Model
{
    public class OrderModel
    {
        public int Sn { get; set; }
        public int CustomerNo { get; set; }
        public int ProductNo { get; set; }
        public int Quantity { get; set; }
        public DateTime CreatedTime { get; set; }

        public static OrderModel FindByOrderSn(int sn)
        {
            var orders = SqliteHelper.Query<Order>("select * from 'Order' where Sn = @sn", new { sn = 1 }).ToList();
            
            if(!orders.Any())
            {
                return null;
            }

            var order = orders.First();

            return new OrderModel()
            {
                Sn = order.Sn,
                CustomerNo = order.CustomerNo,
                ProductNo = order.ProductNo,
                Quantity = order.Quantity,
                CreatedTime = DateTime.Parse(order.CreatedTime)
            };
        }
    }
}
