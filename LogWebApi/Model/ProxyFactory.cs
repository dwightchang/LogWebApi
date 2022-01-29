using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProxy.Proxy;

namespace LogWebApi.Model
{
    public class ProxyFactory
    {
        public static OrderProxy OrderProxy()
        {
            return new OrderProxy("http://localhost:63392", "f496896f-446f-4a5b-97c4-b6d12f66a22c");
        }
    }
}
