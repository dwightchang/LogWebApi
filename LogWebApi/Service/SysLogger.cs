using LogWebApi.Enums;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LogWebApi.Service
{
    public class SysLogger
    {
        private static Logger _logger = LogManager.GetLogger("System");
        public static void Info(string msg)
        {
            _logger.Info(msg);
        }

        public static void Error(string msg)
        {
            _logger.Error(msg);
        }

        public static void Fatal(string msg)
        {
            _logger.Fatal(msg);
        }

        public static void Log(string msg, ExceptionSeverity severity)
        {
            switch(severity)
            {
                case ExceptionSeverity.Info:
                    Info(msg);
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
    }
}
