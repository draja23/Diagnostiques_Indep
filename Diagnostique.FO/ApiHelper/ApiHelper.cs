using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Diagnostique.FO.ApiHelper
{
    public class ApiHelper
    {
        public HttpClient Initial()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
               .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
               .AddJsonFile("appsettings.json")
               .Build();       

            var client = new HttpClient
            {
                BaseAddress = new Uri(configuration.GetSection("ApiBasePath").GetSection("Path").Value)
            };
            return client;
        }


        public async Task<T> GetApiAsync<T>(string url)
        {
            T _objType = default(T);
            try
            {
                HttpClient client = this.Initial();
                HttpResponseMessage res = await client.GetAsync(url);
                if (res.IsSuccessStatusCode)
                {
                    var returnResult = res.Content.ReadAsStringAsync().Result;
                    _objType = JsonConvert.DeserializeObject<T>(returnResult);
                }
                return _objType;
            }
            catch
            {
                return default(T);
            }
        }
        

        public async Task<T> PostApiAsync<T, U>(string url, U datas)
        {
            T _objType = default(T);
            try
            {
                HttpClient client = this.Initial();

                //var myContent = JsonConvert.SerializeObject(datas);
                //var buffer = System.Text.Encoding.UTF8.GetBytes(myContent);
                //var srcDonnee = new ByteArrayContent(buffer);
                //srcDonnee.Headers.ContentType = new MediaTypeHeaderValue("application/json");

             StringContent srcDonnee = new StringContent(JsonConvert.SerializeObject(datas), Encoding.UTF8, "application/json");

                HttpResponseMessage res = await client.PostAsync(url, srcDonnee);
                if (res.IsSuccessStatusCode)
                {
                    var returnResult = res.Content.ReadAsStringAsync().Result;
                    _objType = JsonConvert.DeserializeObject<T>(returnResult);
                }
                return _objType;
            }
            catch
            {
                return default(T);
            }
        }
    }
}
