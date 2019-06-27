using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Payload;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic;
using UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement;
using UIC.Communication.M2mgo.CommunicationAgent.Translation.Project;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi;
using UIC.Communication.M2mgo.CommunicationAgent.WebApi.infrastructure;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent
{
    public class M2mgoCommunicationAgentImpl : Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        private readonly ISerializer _serializer;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        private readonly M2MgoCloudAgentConfiguration _configuration;
        private readonly MqttWarapper _mqttWarapper;
        private readonly MqttConnectionWatchdog _mqttConnectionWatchdog;

        private ITopic _projectDataTopic;
        private ITopic _projectAttributeTopic;
        private ITopic _gatewayDataTopic;
        private ApplianceBlueprints _applianceBlueprints;
        private M2MgoProjectBlueprintTranslator _m2MgoProjectBlueprintTranslator;


        public M2mgoCommunicationAgentImpl(ISerializer serializer, ILoggerFactory loggerFactory) {
            _serializer = serializer;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());
            _configuration = ConfigurationProvider.GetConfiguration(serializer, _logger);


            _mqttConnectionWatchdog = new MqttConnectionWatchdog(loggerFactory.GetLoggerFor(typeof(MqttConnectionWatchdog)));
            _mqttWarapper = new MqttWarapper(_mqttConnectionWatchdog, loggerFactory.GetLoggerFor(typeof(MqttWarapper)));

            
        }



        public void Connect(Action<Command> commandHandler) {
            _logger.Information("connect");

            if (commandHandler == null) throw new ArgumentNullException("commandHandler");
            if (_m2MgoProjectBlueprintTranslator == null) throw new ApplicationException("Initialize was not called before connection to PST");
            var param = new M2mgoMqttParams(_configuration.BrokerBaseUrl);
            _mqttWarapper.Connect(param, _m2MgoProjectBlueprintTranslator, command => new Thread(() => commandHandler(command)).Start());
            Debug(GetType() + " started");
        }

        public void Dispose() {
            _mqttConnectionWatchdog?.Dispose();
            _mqttWarapper?.Dispose();
        }

        // UIC Project ist ein Konfigurationsobjekt, hier sind alle Informationen über das lokale Setup
        // edms: liste aller verbundenen Devices. Wie sie angesteuert werden ist im UIC beschrieben. Ist für den Weg zurück wichtig
        public void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms) {
            WebApiRequestExecutor webApiRequestExecutor = new WebApiRequestExecutor();
            M2mgoUserTokenWebApiWrapper userTokenWebApiWrapper = new M2mgoUserTokenWebApiWrapper(_serializer, webApiRequestExecutor, _loggerFactory.GetLoggerFor(typeof(M2mgoUserTokenWebApiWrapper)));
            var m2MgoGatewayProjectWebApiWrapper = new M2mgoGatewayProjectWebApiWrapper(_serializer, _loggerFactory, webApiRequestExecutor, userTokenWebApiWrapper);
            M2MgoProjectTranslator projectTranslator = new M2MgoProjectTranslator();
            var gatewayService = new GatewayService(m2MgoGatewayProjectWebApiWrapper, _loggerFactory.GetLoggerFor(typeof(GatewayService)), _serializer, projectTranslator);
            
            M2mgoGetwayProjectDto gatewayProject = gatewayService.RegisterProject(_configuration, serialId, project);

            M2mgoDeviceWebApiWrapper apiWrapper = new M2mgoDeviceWebApiWrapper(_serializer, webApiRequestExecutor, _loggerFactory.GetLoggerFor(typeof(M2mgoDeviceWebApiWrapper)), userTokenWebApiWrapper);
            M2mgoGatewayBlueprintTranslator blueprintTranslator = new M2mgoGatewayBlueprintTranslator(projectTranslator);
            _m2MgoProjectBlueprintTranslator = new M2MgoProjectBlueprintTranslator(project, edms);
            var blueprintService = new BlueprintService(apiWrapper, _loggerFactory.GetLoggerFor(typeof(BlueprintService)), blueprintTranslator, _m2MgoProjectBlueprintTranslator, projectTranslator, m2MgoGatewayProjectWebApiWrapper);
            
            _applianceBlueprints = blueprintService.SynchronizeWithCloud(_configuration, serialId, project, gatewayProject);
            
            _projectDataTopic = new DataTopic(_applianceBlueprints.ProjectBlueprint.Identifier.ID, serialId);
            _projectAttributeTopic = new AttributeTopic(_applianceBlueprints.ProjectBlueprint.Identifier.ID, serialId);
            _gatewayDataTopic = new DataTopic(_applianceBlueprints.GatewayBlueprint.Identifier.ID, serialId);
            
        }

        // Datapoint Value: Sensordaten verknüpft mit einem Zeitpunkt
        // Attibute Value: Zustandsinformationen. Zeitlich ändert sich es nicht. 
        public void Push(DatapointValue value) {
            Push(new []{value});
        }

        //
        public void Push(IEnumerable<DatapointValue> values) {
            var dtos = values.Select(v => new M2MgoSensorValuePayload.SensorValueDto(_m2MgoProjectBlueprintTranslator.GetKeyFrom(v.Definition), v.Value.ToString()));
            M2MgoPublishMessage msg = new M2MgoPublishMessage(_projectDataTopic, new M2MgoSensorValuePayload(dtos, _serializer));
            _mqttWarapper.Pulish(msg);
        }

        public void Push(AttributeValue value) {
            Push(new []{value});
        }

        public void Push(IEnumerable<AttributeValue> values) {
            var dtos = values.Select(v => new M2MgoAttributeValuePayload.AttributeValueDto(_m2MgoProjectBlueprintTranslator.GetKeyFrom(v.Definition), v.Value.ToString()));
            M2MgoPublishMessage msg = new M2MgoPublishMessage(_projectAttributeTopic, new M2MgoAttributeValuePayload(dtos, _serializer));
            _mqttWarapper.Pulish(msg);
        }

        // Nur falls im System was passiert wird ein Debugger aufgerufen. Wird vernachlässigt.
        public void Debug(string debug) {
            if (debug == null)
                throw new ArgumentNullException("debug");
            
            var payload = new M2MgoSensorValuePayload.SensorValueDto(M2mgoGatewayBlueprintTranslator.SensorKeyDebug, debug);
            var msg = new M2MgoPublishMessage(_gatewayDataTopic, new M2MgoSensorValuePayload(payload, _serializer));
            
            _mqttWarapper.Pulish(msg);
        }
    }
}