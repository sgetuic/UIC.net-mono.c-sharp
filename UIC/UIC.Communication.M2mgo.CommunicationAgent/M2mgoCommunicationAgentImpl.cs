using System;
using System.Collections.Generic;
using System.Threading;
using UIC.Communication.M2mgo.CommunicationAgent.Configuration;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Logging;
using UIC.Util.Resolver;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent
{
    public class M2mgoCommunicationAgentImpl : Framework.Interfaces.Communication.CommunicationAgent
    {
        private readonly ILogger _logger;
        private readonly M2MgoCloudAgentConfiguration _configuration;
        private readonly MqttWarapper _mqttWarapper;
        private readonly MqttConnectionWatchdog _mqttConnectionWatchdog;

        public M2mgoCommunicationAgentImpl(IResolver resolver) {
            _logger = resolver.GetLoggerFor(GetType());
            var serializer = resolver.Get<ISerializer>();
            _configuration = ConfigurationProvider.GetConfiguration(serializer, _logger);


            _mqttConnectionWatchdog = new MqttConnectionWatchdog(resolver.GetLoggerFor(typeof(MqttConnectionWatchdog)));
            _mqttWarapper = new MqttWarapper(_mqttConnectionWatchdog, resolver.GetLoggerFor(typeof(MqttWarapper)));
        }

        public void Connect(Action<Command> commandHandler) {
            _logger.Information("connect");
            if (commandHandler == null) throw new ArgumentNullException("commandHandler");
            var param = new M2mgoMqttParams(_configuration.BrokerBaseUrl);
            throw new NotImplementedException();
            //_mqttWarapper.Connect(param, command => new Thread(() => commandHandler(command)).Start());
        }

        public void Dispose() {
            _mqttConnectionWatchdog?.Dispose();
            _mqttWarapper?.Dispose();
        }

        public void Synchronize(string serialId, UicProject project) {
            throw new NotImplementedException();
        }

        public void Push(DatapointValue value) {
            Push(new []{value});
        }

        public void Push(IEnumerable<DatapointValue> values) {
            throw new NotImplementedException();
        }

        public void Push(AttributeValue value) {
            Push(new []{value});
        }

        public void Push(IEnumerable<AttributeValue> values) {
            throw new NotImplementedException();
        }

        public void Debug(string debug) {
            // do nothing yet
        }
    }
}