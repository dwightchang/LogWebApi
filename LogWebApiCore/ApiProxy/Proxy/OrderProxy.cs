using ApiProxy.Models.Order;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace ApiProxy.Proxy
{
    public class OrderProxy : ProxyBase
    {
        public OrderProxy(string ApiRootUrl, string ApiToken): base(ApiRootUrl, ApiToken)
        {
            
        }

        public FindOrderResult FindOrder(FindOrderReq req)
        {
            var data = new ApiRequestData()
            {
                apiPath = "Order/FindOrder",
                contentType = ApiRequestData.ContentType.application_json,
                method = ApiRequestData.Method.POST,
                postData = JsonConvert.SerializeObject(req)
            };

            var resp = InvokeApi<FindOrderResp>(data);

            if (!resp.Success)
            {
                throw new Exception(JsonConvert.SerializeObject(resp));
            }

            return resp.Result;
        }
    }
}
