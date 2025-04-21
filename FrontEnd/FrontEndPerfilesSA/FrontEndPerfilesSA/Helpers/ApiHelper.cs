using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace FrontEndPerfilesSA.Helpers
{
    public static class ApiHelper
    {
        private static readonly string conexion = System.Configuration.ConfigurationManager.AppSettings["ApiBaseUrl"];

        private static HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(conexion)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpResponseMessage Post<T>(string endpoint, T data)
        {
            using (var client = CreateClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return client.PostAsync(endpoint, content).Result;
            }
        }
        

        public static HttpResponseMessage Put<T>(string endpoint, T data)
        {
            using (var client = CreateClient())
            {
                var json = JsonConvert.SerializeObject(data);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                return client.PutAsync(endpoint, content).Result;
            }
        }

        public static HttpResponseMessage Get(string endpoint)
        {
            using (var client = CreateClient())
            {
                return client.GetAsync(endpoint).Result;
            }
        }

        public static HttpResponseMessage Delete(string endpoint)
        {
            using (var client = CreateClient())
            {
                return client.DeleteAsync(endpoint).Result;
            }
        }



    }
}
