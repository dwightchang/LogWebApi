using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProxy
{
    public interface IApiLogger
    {
        void Info(string msg);
        void Debug(string msg);
        void Error(string msg);
        void LogElapsedTime(string msg);
    }
}
