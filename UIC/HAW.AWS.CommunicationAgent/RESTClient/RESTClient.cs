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
    }
}
