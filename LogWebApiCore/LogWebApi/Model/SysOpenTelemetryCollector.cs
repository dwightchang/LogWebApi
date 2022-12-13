using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LogWebApi.Model.Log;
using LogWebApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LogWebApi.Model
{
    public class SysOpenTelemetryCollector
    {
        public static readonly string SourceName = "SysTelemetry";
        public static readonly List<Activity> ExportedItems = new List<Activity>();
        public static readonly ActivitySource MyActivitySource = new ActivitySource(SourceName);

        private static object _exportLock = new object();
        private static bool _processing = false;

        public static async void ExportTelemetryTask(ITraceHelper traceHelper)
        {
            await Task.Run(() => ExportTelemetry(traceHelper));
        }

        public static void ExportTelemetry(ITraceHelper traceHelper)
        {
            if (_processing)
            {
                return;
            }

            lock (_exportLock)
            {
                _processing = true;

                try
                {
                    foreach (var item in ExportedItems.ToArray())
                    {
                        try
                        {
                            if (item == null)
                            {
                                continue;
                            }

                            ExportedItems.Remove(item);
                            var traceId = item.Tags.FirstOrDefault(t => t.Key.Equals("TraceId")).Value;

                            //if (contextPocketHolder.ContextPocket != null)
                            //    traceId = contextPocketHolder.ContextPocket.TraceId;

                            var telemetry = new LogItem()
                            {
                                TraceId = traceId,
                                SpanId = item.SpanId.ToString(),
                                StartTime = item.StartTimeUtc.ToLocalTime(),
                                SourceName = item.Source.Name,
                                ActivityName = item.DisplayName,
                                Duration = (long) item.Duration.TotalMilliseconds,
                                Url = item.Tags.FirstOrDefault(t => t.Key.Equals("http.url")).Value,
                                Clientip = item.Tags.FirstOrDefault(t => t.Key.Equals("Clientip")).Value
                            };

                            SysLogger.System.Log(telemetry);
                        }
                        catch (Exception e)
                        {
                            SysLogger.System.Error($"ExportTelemetry fail, {JsonConvert.SerializeObject(item)}, {e}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    SysLogger.System.Error(ex.ToString());
                }
                finally
                {
                    _processing = false;
                }
            }
        }

        public static void AspNetCoreEnrich(Activity activity, string eventName, object rawObject)
        {
            //if (eventName.Equals("OnStartActivity"))
            //{
            //    if (rawObject is HttpRequest httpRequest)
            //    {
            //        var logger =
            //            httpRequest.HttpContext.RequestServices.GetRequiredService<FilebeatLogger>() as FilebeatLogger;
            //        var traceId = logger?.TraceId;

            //        activity.SetTag("TraceId", traceId);
            //        activity.SetTag("Clientip",
            //            httpRequest.HttpContext?.Connection?.RemoteIpAddress?.ToString());
            //    }
            //}
        }

        public static void HttpClientEnrich(Activity activity, string eventName, object rawObject)
        {
            if (eventName.Equals("OnStartActivity"))
            {
                var act = activity;
                if (rawObject is HttpRequestMessage request)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (act.Parent == null)
                        {
                            break;
                        }

                        act = act.Parent;
                    }
                }

                var traceId = act.Tags.FirstOrDefault(t => t.Key.Equals("TraceId")).Value;
                activity.SetTag("TraceId", traceId);
            }
        }
    }
}
