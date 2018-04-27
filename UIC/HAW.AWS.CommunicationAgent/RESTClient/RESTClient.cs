using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace HAW.AWS.CommunicationAgent.RESTClient
{
    static class RESTClient
    {
        static HttpClient client = new HttpClient();

       async public static void Push(String msg)
        {
            // Update port # in the following line.
            client.BaseAddress = new Uri("http://localhost:8080/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.PutAsJsonAsync($"rest/iot/, "+msg);
            response.EnsureSuccessStatusCode();

        }
    }
}
