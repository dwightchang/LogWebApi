using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
            throw new Exception("order fail");
        }
    }
}