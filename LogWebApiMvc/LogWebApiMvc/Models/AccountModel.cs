using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LogWebApiMvc.Models.Log;

namespace LogWebApiMvc.Models
{
    public class AccountModel
    {
        public static int FindAccountId(string name)
        {
            System.Threading.Thread.Sleep(5000);
            SysLogger.System.Info($"FindAccountId from {name}");
            return 1;
        }
    }
}