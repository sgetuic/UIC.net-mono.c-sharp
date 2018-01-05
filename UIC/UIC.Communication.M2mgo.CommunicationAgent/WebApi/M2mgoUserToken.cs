using System.Net;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    public class M2mgoUserToken
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
        public string TokenPrefix { get; set; }

        public void AddAuthorizationHeader(WebRequest request)
        {
            request.Headers.Add("Authorization", TokenPrefix + Token);
        }
    }
}