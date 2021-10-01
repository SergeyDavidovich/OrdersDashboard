using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrdersDashboard.Helpers
{
    public class NoCorsRequestHelper
    {
        public HttpRequestMessage GetRequest(string addres)
        {
            var request = new HttpRequestMessage()
            {
                RequestUri = new Uri(addres),
                Method = HttpMethod.Get
            };
            request.Headers.Add("mode", "no-cors");
            return request;
        }
    }
}
