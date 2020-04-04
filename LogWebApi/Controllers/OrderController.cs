using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LogWebApi.Exceptions;
using LogWebApi.Model;
using Microsoft.AspNetCore.Mvc;

namespace LogWebApi.Controllers
{
    public class OrderController : Controller
    {
        public IActionResult Index()
        {
            return Content("OrderController");
        }

        public IActionResult CreateOrder(int productNo)
        {
            if(productNo <= 0)
            {
                throw new InvalidProductNoException(productNo);
            }

            return Json(new ResponseViewModel<string>()
            {
                Success = true,
                Message = "Order was created",
                Result = ""
            });
        }

        public IActionResult FindOrder(int orderSn)
        {
            var order = OrderModel.FindByOrderSn(orderSn);

            return Json(new ResponseViewModel<OrderModel>
            {
                Success = true,
                Result = order
            });
        }
    }
}