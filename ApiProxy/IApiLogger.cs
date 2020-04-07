using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProxy
{
    public interface IApiLogger
    {
        void info(string title, string msg);
        void debug(string title, string msg);
        void error(string title, string msg);
        void logElapsedTime(string title, string msg);
    }
}
