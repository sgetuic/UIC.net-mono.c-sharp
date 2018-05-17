using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using UIC.Util.Logging;

namespace HAW.AWS.CommunicationAgent.RESTClient
{
    static class RESTClient
    {
        private static String buffer = "";
        private static int msgID = 0;

     
public static async Task PushAsync(String msg, ILogger logger)
        {
            try
            {
                HttpClient client = new HttpClient();
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.GetAsync($"rest/iot/legacy/" + msg);
                msgID++;
                response.EnsureSuccessStatusCode();
                logger.Information("[DEBUG] pushing to:" + "rest/iot/legacy" + msg);
                buffer = "";
            }
            catch
            {
                logger.Error("REST Connection failed");
                buffer = buffer + msg;
            }

        }

        public static async Task PostAsync(String msg, ILogger logger)
        {
            try
            {
                HttpClient client = new HttpClient();
                // Update port # in the following line.
                client.BaseAddress = new Uri("http://localhost:8080/");
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var content = new StringContent(msg, Encoding.UTF8, "application/json");
                var response = await client.PostAsync("rest/iot/push", content);
                msgID++;
                //response.EnsureSuccessStatusCode();
                logger.Information("[DEBUG] pushing to:" + "rest/iot/legacy" + msg);
                buffer = "";
            }
            catch
            {
                logger.Error("REST Connection failed");
                buffer = buffer + msg;
            }

        }


        public static void Initialize(String serialID, String msg, ILogger logger)
        {
            try
            {
                String url = "http://localhost:8080/";
                String path = "/rest/iot/init";

                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri(url);
                    client.DefaultRequestHeaders.Accept.Clear();
                    var content = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("uic_id", serialID)
                    });
                    var response = client.PostAsync(path, content).Result;
                    string resultContent = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine(resultContent);
                    response.EnsureSuccessStatusCode();
                    logger.Information("[DEBUG] Initialize on:" + client.BaseAddress + path + " with content: " + msg);
                }
            }
            catch
            {
                logger.Error("Initialize failed");
            }
        }
    }
}
    


