using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace ApiProxy
{
    public class ProxyBase
    {
        public IApiLogger Logger { get; set; }
        public string ApiRootUrl { get; set; }
        public string ApiToken { get; set; }        

        protected ApiRequestData makeRequestData(string apiPath, object request)
        {
            Dictionary<string, string> headers = new Dictionary<string, string>
            {
                { "ApiToken", ApiToken }
            };

            ApiRequestData data = new ApiRequestData()
            {
                apiPath = apiPath,
                headers = headers,
                postData = JsonConvert.SerializeObject(request)
            };

            return data;
        }

        protected T invokeApi<T>(ApiRequestData data) where T : new()
        {
            try
            {
                string apiUrl = ApiRootUrl.TrimEnd('/') + "/" + data.apiPath;

                Stopwatch watch = Stopwatch.StartNew();
                T resp = invokeApi<T>(apiUrl, data, this.Logger);
                logElapsedTime(watch, apiUrl, data.postData);

                return resp;
            }
            catch (Exception ex)
            {
                Logger.error(data.apiPath, ex.ToString());

                T resp = new T();
                return resp;
            }
        }

        private void logElapsedTime(Stopwatch watch, string url, string postData)
        {
            try
            {
                watch.Stop();

                decimal seconds = ((decimal)watch.ElapsedMilliseconds) / 1000m;              

                string msg = string.Format("{0} seconds, {1}, {2}", seconds.ToString("0.#"), url, postData ?? "");

                Match m = Regex.Match(url, "http[s]?://(.*)[?]");

                if (m.Success == false)
                {
                    m = Regex.Match(url, "http[s]?://(.*)/");
                }

                string title = "";

                if (m.Success)
                {
                    title = m.Groups[1].Value;
                }

                Logger.logElapsedTime(title, msg);
            }
            catch { }
        }

        public T invokeApi<T>(string url, ApiRequestData data, IApiLogger logger)
        {
            string value = "";

            try
            {
                var request = (HttpWebRequest)WebRequest.Create(url);
                request.Credentials = CredentialCache.DefaultCredentials;

                request.Timeout = data.requestTimeout;

                // headers
                if (data.acceptHeader != null)
                {
                    request.Accept = data.acceptHeader;
                }

                foreach (KeyValuePair<string, string> header in data.headers)
                {
                    request.Headers.Add(header.Key, header.Value);
                }

                //處理POST資料
                if (data.method == ApiRequestData.Method.POST ||
                    data.method == ApiRequestData.Method.PUT)
                {
                    var paramBytes = Encoding.UTF8.GetBytes(data.postData);

                    request.Method = data.method == ApiRequestData.Method.POST ? "POST" : "PUT";
                    request.ContentType = data.contentTypeText;
                    request.ContentLength = paramBytes.Length;
                    using (var stream = request.GetRequestStream())
                    {
                        stream.Write(paramBytes, 0, paramBytes.Length);
                    }
                }
                else if (data.method == ApiRequestData.Method.GET)
                {
                    request.Method = "GET";
                }

                logger.info(url, $"req: {data.postData}");
                
                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    //HttpStatusCode=200才算呼叫成功
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        using (var sr = new StreamReader(response.GetResponseStream()))
                        {
                            value = sr.ReadToEnd();
                        }
                    }
                    else
                    {
                        throw new Exception(((int)response.StatusCode).ToString() + ":" + response.StatusDescription);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.error("invokeApi", ex.ToString());
            }

            logger.info(url, $"resp: {value}");

            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}
