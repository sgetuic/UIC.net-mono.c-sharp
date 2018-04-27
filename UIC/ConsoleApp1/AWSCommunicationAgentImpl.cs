using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using UIC.Framework.Interfaces.Communication.Application;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Logging;
using UIC.Util.Serialization;

public class AWSCommunicationAgentImpl : UIC.Framework.Interfaces.Communication.Application.CommunicationAgent
{
    private ISerializer _serializer;
    private ILoggerFactory _loggerFactory;
    private ILogger _logger;
    private AWSCloudAgentConfiguration _configuration;
    private static readonly HttpClient client = new HttpClient();
    private M2MgoProjectBlueprintTranslator _m2MgoProjectBlueprintTranslator;

    public AWSCommunicationAgentImpl(ISerializer serializer, ILoggerFactory loggerFactory)
    {
        _serializer = serializer;
        _loggerFactory = loggerFactory;
        _logger = loggerFactory.GetLoggerFor(GetType());
    }


    void CommunicationAgent.ConnectAsync(Action<Command> commandHandler)
    {
          if (commandHandler == null) throw new ArgumentNullException("commandHandler");
        if (_m2MgoProjectBlueprintTranslator == null) throw new ApplicationException("Initialize was not called before connection to PST");

        var values = new Dictionary<string, string>
{
             { "messageType", "Information" },
   { "status", "connecting" }

};

        PostMessageAsync(values);

    }



    public void Debug(string debug)
    {
        var values = new Dictionary<string, string>
{
             { "messageType", "Request" },
   { "message", debug }

};
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
    private void PostMessageAsync(Dictionary<string, string> values)
    {
       


            var content = new FormUrlEncodedContent(values);

            var response = client.PostAsync(_configuration.BaseUrl, content);

            var responseString = response.Content.ReadAsStringAsync();
            System.Diagnostics.Debug.WriteLine(responseString);
        
    }

    
}
