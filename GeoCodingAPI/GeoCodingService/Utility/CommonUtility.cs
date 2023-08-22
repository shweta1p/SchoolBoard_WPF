using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace GeoCodingService.Utility
{
    public class CommonUtility
    {
        public string GeoAPICall(string address)
        {
            string baseUrl = ConfigurationManager.AppSettings["URL"];
            string key = ConfigurationManager.AppSettings["Key"];
            string finalURL = baseUrl + "address=" + address +"&key="+key;

            string response = PostAsyncJson(finalURL);

            return response;
        }
        public string NearbyPlacesAPICall(string keyword, string parameters)
        {
            string baseUrl = ConfigurationManager.AppSettings["MapURL"];
            string key = ConfigurationManager.AppSettings["Key"];
            string finalURL = baseUrl + "keyword=" + keyword + "&location=" + parameters + "&radius=15000" + "&key=" + key;

            string response = PostAsyncJson(finalURL);

            return response;
        }

        public string PostAsyncJson(string baseUrlForApi)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                try
                {
                    ServicePointManager.Expect100Continue = true;
                    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                    client.BaseAddress = new Uri(baseUrlForApi);
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    var httpContent = new StringContent("", Encoding.UTF8, "application/json");
                    HttpResponseMessage response = client.PostAsync(baseUrlForApi, httpContent).Result;

                    string result = "false";
                    if (response.IsSuccessStatusCode == false)
                    {
                        if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                            result = "NOT_FOUND";
                    }
                    else
                        result = response.Content.ReadAsStringAsync().Result;

                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

    }
}
