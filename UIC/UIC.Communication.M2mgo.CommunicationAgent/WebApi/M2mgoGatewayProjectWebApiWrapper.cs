using System;
using System.Collections.Generic;
using System.Net;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.infrastructure;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Extensions;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    internal class M2mgoGatewayProjectWebApiWrapper
    {
        private readonly ISerializer _serializer;
        private readonly WebApiRequestExecutor _webApiRequestExecutor;
        private readonly ILogger _logger;
        private readonly M2mgoUserTokenWebApiWrapper _userTokenWebApiWrapper;


        public M2mgoGatewayProjectWebApiWrapper(ISerializer serializer, ILoggerFactory loggerFactory, WebApiRequestExecutor webApiRequestExecutor, M2mgoUserTokenWebApiWrapper userTokenWebApiWrapper)
        {
            _serializer = serializer;
            _webApiRequestExecutor = webApiRequestExecutor;
            _userTokenWebApiWrapper = userTokenWebApiWrapper;
            _logger = loggerFactory.GetLoggerFor(GetType());
        }

        internal string GetGatewayProject(M2MgoCloudAgentConfiguration config, UicProject project) {

            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {
                var request =
                    (HttpWebRequest) WebRequest.Create(config.BaseUrl + "api/gatewayProject/" +
                                                       project.CustomerForeignKey.ToString("D") + "/" +
                                                       config.SgetGatewayTypeId.ToString("D") + "/" + project.ProjectKey);
                request.Method = "GET";

                string executeRequest = _webApiRequestExecutor.ExecuteRequest(request, string.Empty, _userTokenWebApiWrapper.GetToken(), _logger);
                return executeRequest;

            });
        }

        public string CreateGatewayProject(M2MgoCloudAgentConfiguration config, GatewayProjectPutModel model) {
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {
                if (model == null)
                    throw new ArgumentNullException("model");

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/gatewayProject");
                request.Method = "PUT";
                string serializeObject = _serializer.Serialize(model);
                return _webApiRequestExecutor.ExecuteRequest(request, serializeObject, _userTokenWebApiWrapper.GetToken(), _logger);
            });

        }

        public string GetGateway(M2MgoCloudAgentConfiguration config, Guid gatewayId) {
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/gateway/" + gatewayId.ToString("D"));
                request.Method = "Get";
                return _webApiRequestExecutor.ExecuteRequest(request, null, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }

        public string AuthenticateGateway(M2MgoCloudAgentConfiguration config, string serialId) {
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {
                if (serialId.IsNullOrEmpty())
                    throw new ArgumentNullException("serialId");

                var request = (HttpWebRequest) WebRequest.Create(config.BaseUrl
                                                                 + "api/gateway/authenticate"
                                                                 + "?gatewayTypeId=" + config.SgetGatewayTypeId.ToString("D")
                                                                 + "&serial=" + serialId);

                request.Method = "POST";
                return _webApiRequestExecutor.ExecuteRequest(request, null, _userTokenWebApiWrapper.GetToken(), _logger);

            });

        }

        public string CreateGateway(M2MgoCloudAgentConfiguration config, string serialId, GatewayProjectGetModel project)
        {
            if (serialId == null) throw new ArgumentNullException("serialId");
            if (project == null) throw new ArgumentNullException("project");

            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                
                 var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl 
                    + "api/gateway" 
                    + "?gatewayTypeId=" + config.SgetGatewayTypeId.ToString("D")
                    + "&domainId=" + project.Domain.ID
                    + "&serial=" + serialId);
                request.Method = "PUT";
                return _webApiRequestExecutor.ExecuteRequest(request, null, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }

        public string AddGatewayToProject(M2MgoCloudAgentConfiguration config, Guid gatewayGuid, Guid projectGuid) {
            if(gatewayGuid == Guid.Empty)
                throw new Exception("gateway guid must not be empty");
            if (projectGuid == Guid.Empty)
                throw new Exception("project guid must not be empty");

            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {

                var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl
                    + "api/gateway"
                    + "/" + gatewayGuid.ToString("D")
                    + "/gatewayProject"
                    + "?gatewayProjectIdentifier=" + projectGuid.ToString("D"));
                request.Method = "POST";
                return _webApiRequestExecutor.ExecuteRequest(request, null, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }

        public string PostRelevantDevices(M2MgoCloudAgentConfiguration config, Guid gatewayGuid, IEnumerable<RelevantDevice> relevantDevices) {
            if (relevantDevices == null)
                throw new ArgumentNullException("relevantDevices");
            if (gatewayGuid == Guid.Empty)
                throw new Exception("gateway guid must not be empty");
          
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () => {

                var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl
                    + "api/gateway"
                    + "/" + gatewayGuid.ToString("D")
                    + "/relevantDevices");
                request.Method = "POST";
                string serializeObject = _serializer.Serialize(relevantDevices);
                return _webApiRequestExecutor.ExecuteRequest(request, serializeObject, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }
    }
}
