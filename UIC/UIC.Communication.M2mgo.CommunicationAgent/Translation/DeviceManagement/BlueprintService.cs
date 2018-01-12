using System;
using System.Linq;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.Translation.Project;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement {
    internal class BlueprintService {
        private readonly M2mgoGatewayProjectWebApiWrapper _gatewayApiWrapper;
        private readonly M2mgoDeviceWebApiWrapper _apiWrapper;
        private readonly ILogger _logger;
        private readonly M2mgoGatewayBlueprintTranslator _m2MgoGatewayBlueprintTranslator;
        private readonly M2MgoProjectBlueprintTranslator _projectBlueprintTranslator;
        private readonly M2MgoProjectTranslator _projectTranslator;
        

        public BlueprintService(M2mgoDeviceWebApiWrapper apiWrapper, ILogger logger, M2mgoGatewayBlueprintTranslator m2MgoGatewayBlueprintTranslator, M2MgoProjectBlueprintTranslator projectBlueprintTranslator, M2MgoProjectTranslator projectTranslator, M2mgoGatewayProjectWebApiWrapper gatewayApiWrapper)
        {
            _apiWrapper = apiWrapper;
            _logger = logger;
            _m2MgoGatewayBlueprintTranslator = m2MgoGatewayBlueprintTranslator;
            _projectBlueprintTranslator = projectBlueprintTranslator;
            _projectTranslator = projectTranslator;
            _gatewayApiWrapper = gatewayApiWrapper;
        }

        public ApplianceBlueprints SynchronizeWithCloud(M2MgoCloudAgentConfiguration config, string serialId, UicProject project, M2mgoGetwayProjectDto m2MgoGetwayProjectDto)
        {
            BlueprintDto gatewayBlueprint = EnsureGatewayBlueprint(config, project);
            EnsureDevice(config, serialId, gatewayBlueprint, m2MgoGetwayProjectDto.Gateway);
            UpdateGatewayAttributes(config, serialId, project, gatewayBlueprint);

            BlueprintDto projectBlueprint = EnsureProjectBlueprint(config, project);
            EnsureDevice(config, serialId, projectBlueprint, m2MgoGetwayProjectDto.Gateway);
            UpdateProjectDeviceAttributes(config, serialId, projectBlueprint);

            SetRelevantDevicesOfGateway(config, m2MgoGetwayProjectDto.Gateway.Identifier.ID, projectBlueprint, serialId);

            return new ApplianceBlueprints(gatewayBlueprint, projectBlueprint);
        }

        private void SetRelevantDevicesOfGateway(M2MgoCloudAgentConfiguration config, Guid gatewayDevice, BlueprintDto projectBlueprint, string serialId)
        {
            var relevantSensors = projectBlueprint.Sensors.Select(s => new RelevantSensor
            {
                DisplayChartLeftAxis = true,
                DisplayChartRightAxis = false,
                DisplayInTable = true,
                Label = s.Label,
                Path = "",
                SensorKey = s.SensorKey
            }).ToArray();

            var relevantDevice = new RelevantDevice
            {
                DeviceCustomID = serialId,
                DeviceTypeDomain = projectBlueprint.DomainIdentifier.ID,
                DeviceTypeName = projectBlueprint.Code,
                RelevantSensors = relevantSensors
            };

            _gatewayApiWrapper.PostRelevantDevices(config, gatewayDevice, new []{relevantDevice});
        }

        private void UpdateGatewayAttributes(M2MgoCloudAgentConfiguration config, string serialId, UicProject project, BlueprintDto gatewayBlueprint)
        {
            _apiWrapper.PostAttributes(config, gatewayBlueprint.Identifier.ID, serialId,
                M2mgoGatewayBlueprintTranslator.AttributeKeySerialId, serialId);
            _apiWrapper.PostAttributes(config, gatewayBlueprint.Identifier.ID, serialId,
                M2mgoGatewayBlueprintTranslator.AttributeKeyProjectKey, project.ProjectKey);
            _apiWrapper.PostAttributes(config, gatewayBlueprint.Identifier.ID, serialId,
                M2mgoGatewayBlueprintTranslator.AttributeKeyProjectName, project.Name);
            _apiWrapper.PostAttributes(config, gatewayBlueprint.Identifier.ID, serialId,
                M2mgoGatewayBlueprintTranslator.AttributeKeyCustomer, project.Owner);
        }

        private void UpdateProjectDeviceAttributes(M2MgoCloudAgentConfiguration config, string serialId, BlueprintDto projectBlueprintDto) {
            _apiWrapper.PostAttributes(config, projectBlueprintDto.Identifier.ID, serialId,
                M2mgoGatewayBlueprintTranslator.AttributeKeySerialId, serialId);
        }

        private BlueprintDto EnsureProjectBlueprint(M2MgoCloudAgentConfiguration config, UicProject project)
        {
            string blueprintCode = _projectBlueprintTranslator.GetBlueprintCodeFrom(project);
            BlueprintDto[] searchResult = _apiWrapper.SearchBlueprint(config, _projectTranslator.GetProjectDomain(project), blueprintCode);

            if (searchResult.Count() > 1) {
                throw new Exception(String.Format("found more than 1 project blueprints {0} in Domain {1}", _projectTranslator.GetProjectDomain(project),
                    blueprintCode));
            }
            if (searchResult.Any()) {
                BlueprintDto blueprint = _projectBlueprintTranslator.UpdateProjectDomain(searchResult.Single());
                string result = _apiWrapper.UpdateBlueprint(config, blueprint);
                _logger.Information("Updateing Blueprint: " + result);
                return blueprint;
            } else {
                BlueprintDto projectBlueprintDto = _projectBlueprintTranslator.GetProjectBlueprintDto(project, _projectTranslator.GetProjectDomain(project));
                projectBlueprintDto.PrepareMasterForCreation();

                string result = _apiWrapper.CreateBlueprint(config, _projectTranslator.GetProjectDomain(project), projectBlueprintDto);
                _logger.Information("CreateBlueprint Blueprint: " + result);
                return _apiWrapper.SearchBlueprint(config, _projectTranslator.GetProjectDomain(project), blueprintCode).Single();
            }
        }

        private void EnsureDevice(M2MgoCloudAgentConfiguration config, string deviceIdentifier, BlueprintDto blueprint, GatewayGetModel gateway) {
            _apiWrapper.AuthenticateDevice(config, blueprint.Identifier.ID, deviceIdentifier, gateway.Identifier.ID);
        }

        private BlueprintDto EnsureGatewayBlueprint(M2MgoCloudAgentConfiguration config, UicProject project)
        {
            BlueprintDto[] searchResult = _apiWrapper.SearchBlueprint(config, config.SgetDomainID, M2mgoGatewayBlueprintTranslator.CloudMapperGatewayAnchorBlueprintCode);

            if (searchResult.Count() > 1) {
                throw new Exception(String.Format("found more than 1 masterdefinitions {0} in Domain {1}", _projectTranslator.GetProjectDomain(project),
                    M2mgoGatewayBlueprintTranslator.CloudMapperGatewayAnchorBlueprintCode));
            }
            if (searchResult.Any())
            {
                _logger.Information("Gateway Blueprint {0} alredy exists.", M2mgoGatewayBlueprintTranslator.CloudMapperGatewayAnchorBlueprintCode);
                return searchResult.Single();
            }
            BlueprintDto gatewayBlueprintDefiniton = _m2MgoGatewayBlueprintTranslator.GetGatewayBlueprintDefiniton(project);
            gatewayBlueprintDefiniton.PrepareMasterForCreation();

            string result = _apiWrapper.CreateBlueprint(config, _projectTranslator.GetProjectDomain(project), gatewayBlueprintDefiniton);
            _logger.Information("CreateBlueprint Blueprint: " + result);
            return _apiWrapper.SearchBlueprint(config, _projectTranslator.GetProjectDomain(project), M2mgoGatewayBlueprintTranslator.CloudMapperGatewayAnchorBlueprintCode).Single();
        }
    }
}
