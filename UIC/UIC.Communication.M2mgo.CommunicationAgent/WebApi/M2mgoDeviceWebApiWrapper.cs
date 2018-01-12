using System;
using System.Net;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Device;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.infrastructure;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    internal class M2mgoDeviceWebApiWrapper
    {
        private readonly ISerializer _serializer;
        private readonly WebApiRequestExecutor _webApiRequestExecutor;
        private readonly ILogger _logger;
        private readonly M2mgoUserTokenWebApiWrapper _userTokenWebApiWrapper;
        

        public M2mgoDeviceWebApiWrapper(ISerializer serializer, WebApiRequestExecutor webApiRequestExecutor, ILogger logger, M2mgoUserTokenWebApiWrapper userTokenWebApiWrapper)
        {
            _serializer = serializer;
            _webApiRequestExecutor = webApiRequestExecutor;
            _logger = logger;
            _userTokenWebApiWrapper = userTokenWebApiWrapper;
        }

        internal BlueprintDto[] SearchBlueprint(M2MgoCloudAgentConfiguration config, Guid domainId, string deviceTypePattern)
        {
            if (deviceTypePattern == null)
                throw new ArgumentNullException("deviceTypePattern");

            string result = _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/devicetype/search/" +
                                      domainId.ToString("D") + "/" + deviceTypePattern);
                request.Method = "GET";
                return result = _webApiRequestExecutor.ExecuteRequest(request, string.Empty, _userTokenWebApiWrapper.GetToken(), _logger);
            });
            return _serializer.Deserialize<BlueprintDto[]>(result);
        }

        internal BlueprintDto GetBlueprint(M2MgoCloudAgentConfiguration config, Guid blueprintId)
        {
            string result = _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/devicetype/" + blueprintId.ToString("D"));
                request.Method = "GET";
                return _webApiRequestExecutor.ExecuteRequest(request, string.Empty, _userTokenWebApiWrapper.GetToken(), _logger);
            });

            return _serializer.Deserialize<BlueprintDto>(result);
        }

        internal string CreateBlueprint(M2MgoCloudAgentConfiguration config, Guid domainId, BlueprintDto blueprint)
        {
            if (blueprint == null)
                throw new ArgumentNullException("blueprint");
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/devicetype/" + domainId.ToString("D"));
                request.Method = "POST";
                string serializeObject = _serializer.Serialize(blueprint);
                return _webApiRequestExecutor.ExecuteRequest(request, serializeObject, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }

        internal string UpdateBlueprint(M2MgoCloudAgentConfiguration config, BlueprintDto blueprint)
        {
            if (blueprint == null)
                throw new ArgumentNullException("blueprint");
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest)WebRequest.Create(config.BaseUrl + "api/devicetype/" + blueprint.Identifier.ID.ToString("D"));
                request.Method = "PUT";
                string serializeObject = _serializer.Serialize(blueprint);
                return _webApiRequestExecutor.ExecuteRequest(request, serializeObject, _userTokenWebApiWrapper.GetToken(), _logger);

            });
        }

        public Guid AuthenticateDevice(M2MgoCloudAgentConfiguration config, Guid deviceType, string customId, Guid gatewayID)
        {
            var result = _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest) WebRequest.Create(config.BaseUrl
                                                       + "api/device/authentication"
                                                       + "/" + deviceType.ToString("D")
                                                       + "/" + customId
                                                       + "/" + gatewayID.ToString("D"));
                request.Method = "GET";
                return _webApiRequestExecutor.ExecuteRequest(request, null, _userTokenWebApiWrapper.GetToken(),
                    _logger);
                
            });
            _logger.Information("AuthenticateDevice: " + result);
            return _serializer.Deserialize<Guid>(result);
        }

        public string PostAttributes(M2MgoCloudAgentConfiguration config, Guid deviceType, string customId, string attributeName, string value)
        {
            return _userTokenWebApiWrapper.RetryWithTokenUpdate(config, () =>
            {
                var request =
                    (HttpWebRequest) WebRequest.Create(config.BaseUrl
                                                       + "api/device/attribute");
                request.Method = "POST";
                PostAttributeSingleValueModel
                    attribute = new PostAttributeSingleValueModel
                    {
                        AttributeName = attributeName,
                        DeviceName = customId,
                        DeviceType = deviceType,
                        Value = value
                    };
                string serializeObject = _serializer.Serialize(attribute);
                return _webApiRequestExecutor.ExecuteRequest(request, serializeObject, _userTokenWebApiWrapper.GetToken(), _logger);
            });
        }
    }
}
