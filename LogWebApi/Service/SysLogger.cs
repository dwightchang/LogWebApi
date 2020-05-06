using ApiProxy;
using LogWebApi.Enums;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Service
{
    public class SysLogger : IApiLogger
    {
        private Logger _logger;

        protected SysLogger(string loggerName)
        {
            _logger = LogManager.GetLogger(loggerName);
        }

        public static SysLogger System => new SysLogger("System");
        public static SysLogger SqlElapsedTime => new SysLogger("SqlElapsedTime");
        public static SysLogger ApiElapsedTime => new SysLogger("ApiElapsedTime");
        public static SysLogger RequestElapsedTime => new SysLogger("RequestElapsedTime");

        public void Info(string msg)
        {
            _logger.Info(msg);
        }

        public void Warn(string msg)
        {
            _logger.Warn(msg);
        }

        public void Error(string msg)
        {
            _logger.Error(msg);
        }

        public void Fatal(string msg)
        {
            _logger.Fatal(msg);
        }

        public void Log(string msg, ExceptionSeverity severity)
        {
            switch(severity)
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

        public void Debug(string msg)
        {
            _logger.Debug(msg);
        }

        public void LogElapsedTime(string msg)
        {
            _logger.Trace(msg);
        }
    }
}
