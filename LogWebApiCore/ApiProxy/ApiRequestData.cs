using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ApiProxy
{
    public class ApiRequestData
    {
        public enum Method
        {
            GET,
            POST,
            PUT,
            DELETE
        }

        public enum ContentType
        {
            [Description("application/x-www-form-urlencoded")]
            application_x_www_form_urlencoded,

            [Description("application/json")]
            application_json
        }

        public string contentTypeText
        {
            get
            {
                switch(contentType)
                {
                    case ContentType.application_x_www_form_urlencoded:
                        return "application/x-www-form-urlencoded";
                    case ContentType.application_json:
                        return "application/json";
                    default:
                        throw new Exception("unknown content type");
                }
            }
        }        

        public Dictionary<string, string> headers { get; set; }
        public ContentType contentType { get; set; }
        public int requestTimeout { get; set; }
        public string postData { get; set; }
        public string acceptHeader { get; set; }
        public Method method { get; set; }
        public string apiPath { get; set; }

        public ApiRequestData()
        {
            requestTimeout = 600000;
            headers = new Dictionary<string, string>();
            method = Method.POST;
            contentType = ContentType.application_json;
        }
    }
}
