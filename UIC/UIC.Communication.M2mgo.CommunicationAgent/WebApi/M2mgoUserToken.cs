using System;
using System.Net;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    public class M2mgoUserToken
    {
        private DateTime _creationDateTime;
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
        public string TokenPrefix { get; set; }

        public M2mgoUserToken() {
            _creationDateTime = DateTime.UtcNow;
        }
        
        public void AddAuthorizationHeader(WebRequest request)
        {
            request.Headers.Add("Authorization", TokenPrefix + Token);
        }

        internal int GetSecondsSinceTokenCreation() {
            return (int) (DateTime.UtcNow - _creationDateTime).TotalSeconds;
        }
    }
}