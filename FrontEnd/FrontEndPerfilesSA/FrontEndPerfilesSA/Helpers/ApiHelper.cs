using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System;

namespace FrontEndPerfilesSA.Helpers
{
    public static class ApiHelper
    {
        private static HttpClient CreateClient()
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri(ApiRoutes.BaseUrl)
            };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }

        public static HttpResponseMessage Post<T>(string endpoint, T data)
        {
            try
            {
                using (var client = CreateClient())
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PostAsync(endpoint, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Log($"Post correcto");
                    }
                    else
                    {
                        Logger.Log($"Error en POst");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public static HttpResponseMessage Put<T>(string endpoint, T data)
        {
            try
            {
                using (var client = CreateClient())
                {
                    var json = JsonConvert.SerializeObject(data);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");
                    var response = client.PutAsync(endpoint, content).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Log($"Put Correcto.");
                    }
                    else
                    {
                        Logger.Log($"Error en Put");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }
        public static HttpResponseMessage Get(string endpoint)
        {
            try
            {
                using (var client = CreateClient())
                {
                    var response = client.GetAsync(endpoint).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Log($"Get Correcto");
                    }
                    else
                    {
                        Logger.Log($"Error en Get");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }

        public static HttpResponseMessage Delete(string endpoint)
        {
            try
            {
                using (var client = CreateClient())
                {
                    var response = client.DeleteAsync(endpoint).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        Logger.Log($"Delete Correcto");
                    }
                    else
                    {
                        Logger.Log($"Error en delete ");
                    }

                    return response;
                }
            }
            catch (Exception ex)
            {
                Logger.LogError(ex);
                throw;
            }
        }
    }
}
