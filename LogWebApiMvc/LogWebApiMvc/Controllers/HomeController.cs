using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LogWebApiMvc.Models;
using LogWebApiMvc.Models.Log;

namespace LogWebApiMvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Login(string name)
        {
            SysLogger.System.Info($"{name} login");
            AccountModel.FindAccountId(name);
            return Content("OK");
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}