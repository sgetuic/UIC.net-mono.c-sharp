using System;
using System.IO;
using System.Net;
using System.Text;
using UIC.Util.Extensions;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi
{
    internal class WebApiRequestExecutor {

        internal string ExecuteRequest(HttpWebRequest request, string payload, ILogger logger) {
            request.Accept = "*/*";
            request.ServerCertificateValidationCallback = (sender, certificate, chain, errors) => {
                logger.Information("accept server certificate");
                return true;
            };
            if (request.Method != "GET") {
                if (payload.IsNullOrEmpty()) {
                    request.ContentLength = 0;
                }
                else {
                    var data = Encoding.UTF8.GetBytes(payload);
                    var reqStr = request.GetRequestStream();
                    reqStr.Write(data, 0, data.Length);
                    request.ContentType = "application/json";
                }
            }

            logger.Information(request.RequestUri.ToString());

            using (HttpWebResponse resp = (HttpWebResponse) request.GetResponse()) {
                return ReadDataFrom(resp);
            }
        }

        internal static string ReadDataFrom(HttpWebResponse resp) {
            string rawData = String.Empty;
            if (resp.ContentLength > 0) {
                using (var s = new StreamReader(resp.GetResponseStream())) {
                    rawData = s.ReadToEnd();
                }
            }

            if (resp.StatusCode == HttpStatusCode.NotFound) {
                return String.Empty;
            }

            if (resp.StatusCode != HttpStatusCode.OK) {
                throw new Exception(resp.StatusCode + ": " + rawData);
            }

            return rawData;
        }
    }
}