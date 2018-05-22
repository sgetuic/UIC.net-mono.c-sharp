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
using HAW.AWS.CommunicationAgent.Wrapper;
using System.ServiceModel.Web;
using HAW.AWS.CommunicationAgent.RESTClient;

namespace HAW.AWS.CommunicationAgent 
{
    public class HAWCommunicationAgent : UIC.Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        private ISerializer serializer;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;

        public HAWCommunicationAgent(ISerializer serializer, ILoggerFactory loggerFactory)
        {
            this.serializer = serializer;
            this._loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());
            _logger.Information("HAW Communication Agent built.");

            Thread RestServiceThread = new Thread(RestServer.startRESTService);
            RestServiceThread.Start();
        }

        public void Connect(Action<Command> commandHandler)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON("Connect"), _logger);
        }

        public void Debug(string debug)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON(debug), _logger);
        }

        public void Dispose()
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON("Dispose"), _logger);
        }

        public void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms)
        {
            RESTClient.RESTClient.Initialize(serialId, DataAndAttributeValueWrapper.GetJSON("Initialise"), _logger);
        }

        public void Push(DatapointValue value)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON(value), _logger);
        }

        public void Push(IEnumerable<DatapointValue> values)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON(values), _logger);
        }

        public void Push(AttributeValue value)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON(value), _logger);
        }

        public void Push(IEnumerable<AttributeValue> values)
        {
            RESTClient.RESTClient.PostAsync(DataAndAttributeValueWrapper.GetJSON(values), _logger);
        }
    }
}
