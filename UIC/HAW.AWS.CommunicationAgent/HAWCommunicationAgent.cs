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
    class HAWCommunicationAgent : UIC.Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        public void Connect(Action<Command> commandHandler)
        {
            throw new NotImplementedException();
        }

        public void Debug(string debug)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms)
        {
            throw new NotImplementedException();
        }

        public void Push(DatapointValue value)
        {
            throw new NotImplementedException();
        }

        public void Push(IEnumerable<DatapointValue> values)
        {
            throw new NotImplementedException();
        }

        public void Push(AttributeValue value)
        {
            throw new NotImplementedException();
        }

        public void Push(IEnumerable<AttributeValue> values)
        {
            throw new NotImplementedException();
        }
    }
}
