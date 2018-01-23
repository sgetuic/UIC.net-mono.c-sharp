using System;
using System.IO;
using System.Net;
using System.Text;
using UIC.Util.Extensions;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi
{
    internal class WebApiRequestExecutor
    {
        internal string ExecuteRequest(WebRequest request, string fbRequest, ILogger logger) {
            var data = Encoding.UTF8.GetBytes(fbRequest);

            if (request.Method != "GET" && !fbRequest.IsNullOrEmpty())
            {
                request.ContentLength = data.Length;
                var reqStr = request.GetRequestStream();
                reqStr.Write(data, 0, data.Length);
            }

            logger.Information(request.RequestUri.ToString());

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse()) {
                string rawData = String.Empty;
                if (resp.ContentLength > 0)
                {
                    using (var s = new StreamReader(resp.GetResponseStream())) {
                        rawData = s.ReadToEnd();
                    }    
                }
                if (resp.StatusCode != HttpStatusCode.OK)
                {
                    throw new IOException(resp.StatusCode + ": " + rawData);
                }
                logger.Information(rawData);
                return rawData;
            }
        }
    }
}