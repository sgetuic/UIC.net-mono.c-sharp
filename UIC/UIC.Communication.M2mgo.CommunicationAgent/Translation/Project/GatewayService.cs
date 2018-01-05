using System;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Extensions;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.Project {
    internal class GatewayService {
        private readonly M2mgoGatewayProjectWebApiWrapper _apiWrapper;
        private readonly ILogger _logger;
        private readonly ISerializer _serializer;
        private readonly M2MgoProjectTranslator _projectTranslator;

        public GatewayService(M2mgoGatewayProjectWebApiWrapper apiWrapper, ILogger logger, ISerializer serializer, M2MgoProjectTranslator projectTranslator)
        {
            _apiWrapper = apiWrapper;
            _logger = logger;
            _serializer = serializer;
            _projectTranslator = projectTranslator;
        }

        public M2mgoGetwayProjectDto RegisterProject(M2MgoCloudAgentConfiguration config, string serialId, UicProject project)
        {
            GatewayProjectGetModel gatewayProject = EnsureProject(config, project);
            GatewayGetModel gateway = EnsureGateway(config, serialId, gatewayProject);
            return new M2mgoGetwayProjectDto(gateway, gatewayProject);
        }

        private GatewayProjectGetModel EnsureProject(M2MgoCloudAgentConfiguration config, UicProject project)
        {
            string result = _apiWrapper.GetGatewayProject(config, project);
            if (result.IsNullOrEmpty())
            {
                var model = new GatewayProjectPutModel(_projectTranslator.GetProjectDomain(project), config.SgetGatewayTypeId, project.Name, project.ProjectKey, "", project.GetType().ToString());
                _apiWrapper.CreateGatewayProject(config, model);
                result = _apiWrapper.GetGatewayProject(config, project);
                if(result.IsNullOrEmpty())throw new Exception("Could not create GatewayProject!");
            }
            _logger.Information(result);
            var gatewayProjectGetModel = _serializer.Deserialize<GatewayProjectGetModel>(result);
            return gatewayProjectGetModel;
        }


        private GatewayGetModel EnsureGateway(M2MgoCloudAgentConfiguration config, string serialId, GatewayProjectGetModel project)
        {
            string result = _apiWrapper.AuthenticateGateway(config, serialId);
            if (result.IsNullOrEmpty())
            {
                _apiWrapper.CreateGateway(config, serialId, project);
                result = _apiWrapper.AuthenticateGateway(config, serialId);
                if (result.IsNullOrEmpty())
                    throw new Exception("Could not create Gateway!");
            }
            var gatewayId = _serializer.Deserialize<Guid>(result);
            string gatewayResult = _apiWrapper.GetGateway(config, gatewayId);
            var gatewayGetModel = _serializer.Deserialize<GatewayGetModel>(gatewayResult);

            if (gatewayGetModel.GatewayProjectIdentifier == null)
            {
                _apiWrapper.AddGatewayToProject(config, gatewayGetModel.Identifier.ID, project.ID);
                gatewayResult = _apiWrapper.GetGateway(config, gatewayId);
                gatewayGetModel = _serializer.Deserialize<GatewayGetModel>(gatewayResult);
            }
            return gatewayGetModel;
        }
    }
}
