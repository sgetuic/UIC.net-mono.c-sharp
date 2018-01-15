using System;
using System.Net;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.infrastructure;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    internal class M2mgoUserTokenWebApiWrapper
    {
        private readonly ISerializer _serializer;
        private readonly WebApiRequestExecutor _webApiRequestExecutor;
        private readonly ILogger _logger;
        
        private M2mgoUserToken _token;


        public M2mgoUserTokenWebApiWrapper(ISerializer serializer, WebApiRequestExecutor webApiRequestExecutor, ILogger logger)
        {
            _serializer = serializer;
            _webApiRequestExecutor = webApiRequestExecutor;
            _logger = logger;
            
        }

        internal string RetryWithTokenUpdate(M2MgoCloudAgentConfiguration config, Func<string> func)
        {
            if(_token == null) _token = UpdateAccessToken(config);
            try {
                return func();
            }
            catch (WebException e) {
                if (_token.GetSecondsSinceTokenCreation() < 5) throw;
                
                if (e.Response != null && e.Response is HttpWebResponse) {
                    if (((HttpWebResponse) e.Response).StatusCode == HttpStatusCode.NotFound) return string.Empty;
                }

                _token = UpdateAccessToken(config);
                return func();
            }
        }

        private M2mgoUserToken UpdateAccessToken(M2MgoCloudAgentConfiguration config)
        {
            var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "/api/cms/membership-user/token");
            request.Method = "POST";
            request.ContentType = "application/json";
            string serializeObject =
                _serializer.Serialize(new {Email = config.User, Password = config.Password});
            string result = _webApiRequestExecutor.ExecuteRequest(request, serializeObject, null, _logger);
            return _serializer.Deserialize<M2mgoUserToken>(result);
        }


        public M2mgoUserToken GetToken()
        {
            return _token;
        }
    }
}
