using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProxy.Proxy;
using LogWebApi.Model;
using LogWebApi.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogWebApi.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult SalesDashboard()
        {
            var orderData = ProxyFactory.OrderProxy().FindOrder(new ApiProxy.Models.Order.FindOrderReq()
            {
                OrderSn = 1
            });

            return Content(JsonConvert.SerializeObject(orderData));
        }
    }
}