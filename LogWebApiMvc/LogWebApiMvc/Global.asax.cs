using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using LogWebApiMvc.Models;
using LogWebApiMvc.Models.Log;

namespace LogWebApiMvc
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        private void Application_BeginRequest(Object source, EventArgs e)
        {
            TraceHelper.Init();

            // °O¿ýrequest°Ñ¼Æ
            long position = Context.Request.InputStream.Position;
            var data = new StreamReader(Context.Request.InputStream).ReadToEnd();
            Context.Request.InputStream.Position = position;

            var reqLog = new RequestLogData();
            reqLog.UrlPath = Context.Request.Url.AbsolutePath.Split('?')[0].TrimStart('/');
            reqLog.Watch = Stopwatch.StartNew();
            reqLog.RequestData = data;

            Context.Items.Add("RequestLog", reqLog);
        }

        private void Application_EndRequest(Object source, EventArgs e)
        {
            if (!Context.Items.Contains("RequestLog"))
            {
                return;
            }

            RequestLogData reqLog = (RequestLogData)Context.Items["RequestLog"];
            reqLog.Watch.Stop();

            var tags = new LogTag()
            {
                Req = reqLog.RequestData,
            };

            SysLogger.System.Log(new LogItem()
            {
                Type = "Request",
                Duration = reqLog.Watch.ElapsedMilliseconds,
                Level = "Info",
                Message = reqLog.UrlPath,
                Tag = new LogTag()
                {
                    Req = reqLog.RequestData
                }
            });
        }
    }

}
