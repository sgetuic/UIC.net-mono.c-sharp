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
        private readonly M2mgoUserTokenCache _m2MgoUserTokenCache;

        private M2mgoUserToken _token;


        public M2mgoUserTokenWebApiWrapper(ISerializer serializer, WebApiRequestExecutor webApiRequestExecutor, ILogger logger)
        {
            _serializer = serializer;
            _webApiRequestExecutor = webApiRequestExecutor;
            _logger = logger;
            _m2MgoUserTokenCache = new M2mgoUserTokenCache();
            _token = _m2MgoUserTokenCache.Get(_serializer);
        }

        internal string RetryWithTokenUpdate(M2MgoCloudAgentConfiguration config, Func<string> func)
        {
            try {
                return func();
            }
            catch (WebException) {
                UpdateAccessToken(config);
                return func();

            }
        }

        private void UpdateAccessToken(M2MgoCloudAgentConfiguration config)
        {
            var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "/api/cms/membership-user/token");
            request.Method = "POST";
            request.ContentType = "application/json";
            string serializeObject =
                _serializer.Serialize(new {Email = config.User, Password = config.Password});
            string result = _webApiRequestExecutor.ExecuteRequest(request, serializeObject, null, _logger);
            _token = _serializer.Deserialize<M2mgoUserToken>(result);
            _m2MgoUserTokenCache.Cache(result);
        }


        public M2mgoUserToken GetToken()
        {
            return _token;
        }
    }
}
