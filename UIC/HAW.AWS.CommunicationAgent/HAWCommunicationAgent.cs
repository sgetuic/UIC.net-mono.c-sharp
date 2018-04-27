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




namespace HAW.AWS.CommunicationAgent 
{
    public class HAWCommunicationAgent : UIC.Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        private ISerializer serializer;
        private ILoggerFactory loggerFactory;

        public HAWCommunicationAgent(ISerializer serializer, ILoggerFactory loggerFactory)
        {
            this.serializer = serializer;
            this.loggerFactory = loggerFactory;
        }

        public void Connect(Action<Command> commandHandler)
        {
            RESTClient.RESTClient.PushAsync("Connect");
        }

        public void Debug(string debug)
        {
            RESTClient.RESTClient.PushAsync("Debug");
        }

        public void Dispose()
        {
            RESTClient.RESTClient.PushAsync("Dispose");
        }

        public void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms)
        {
            RESTClient.RESTClient.PushAsync("Initialize");
        }

        public void Push(DatapointValue value)
        {
            RESTClient.RESTClient.PushAsync("Push DatapointValue");
        }

        public void Push(IEnumerable<DatapointValue> values)
        {
            RESTClient.RESTClient.PushAsync("Enum<Push DatapointValue>");
        }

        public void Push(AttributeValue value)
        {
            RESTClient.RESTClient.PushAsync("Push AttributeValue>");
        }

        public void Push(IEnumerable<AttributeValue> values)
        {
            RESTClient.RESTClient.PushAsync("Enum<Push AttributeValue>");
        }
    }
}
