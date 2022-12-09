using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogWebApiMvc.Enums;
using Newtonsoft.Json;
using NLog;

namespace LogWebApiMvc.Models.Log
{
    public class SysLogger
    {
        private Logger _logger;

        protected SysLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public static SysLogger System => new SysLogger("System");
        public static SysLogger SqlElapsedTime => new SysLogger("Sqle");
        public static SysLogger ApiElapsedTime => new SysLogger("Api");
        public static SysLogger RequestElapsedTime => new SysLogger("Request");

        public void Info(string msg)
        {
            Log(new LogItem()
            {
                Message = msg,
                Level = "Info",
                Type = "Message"
            });
        }

        public void Warn(string msg)
        {
            Log(new LogItem()
            {
                Message = msg,
                Level = "Warn",
                Type = "Message"
            });
        }

        public void Error(string msg)
        {
            Log(new LogItem()
            {
                Message = msg,
                Level = "Error",
                Type = "Message"
            });
        }

        public void Fatal(string msg)
        {
            Log(new LogItem()
            {
                Message = msg,
                Level = "Fatal",
                Type = "Message"
            });
        }

        public void Log(string msg, ExceptionSeverity severity)
        {
            switch (severity)
            {
                case ExceptionSeverity.Info:
                    Info(msg);
                    break;
                case ExceptionSeverity.Warn:
                    Warn(msg);
                    break;
                case ExceptionSeverity.Error:
                    Error(msg);
                    break;
                case ExceptionSeverity.Fatal:
                    Fatal(msg);
                    break;
                default:
                    Error(msg);
                    break;
            }
        }

        public void Log(LogItem log)
        {
            log.TraceId = TraceHelper.GetTrace().TraceId;
            _logger.Info(JsonConvert.SerializeObject(log));
        }

        public void Debug(string msg)
        {
            Log(new LogItem()
            {
                Message = msg,
                Level = "Debug",
                Type = "Message"
            });
        }

    }
}