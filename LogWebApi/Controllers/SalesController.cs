using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProxy.Proxy;
using LogWebApi.Service;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace LogWebApi.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult SalesDashboard()
        {
            OrderProxy proxy = new OrderProxy("http://localhost:63392", "f496896f-446f-4a5b-97c4-b6d12f66a22c", SysLogger.ApiElapsedTime);
            var orderResp = proxy.FindOrder(new ApiProxy.Models.Order.FindOrderReq()
            {
                OrderSn = 1
            });

            if(orderResp.Success)
            {
                return Content(JsonConvert.SerializeObject(orderResp.Result));
            }
            else
            {
                return Content(orderResp.Message);
            }            
        }
    }
}