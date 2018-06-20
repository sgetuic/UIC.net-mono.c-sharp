using System;
using System.Collections.Generic;
using System.Threading;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Util.Logging;
using UIC.Util.Serialization;
using HAW.AWS.CommunicationAgent.Wrapper;
using HAW.AWS.CommunicationAgent.RESTClient;
using HAW.AWS.CommunicationAgent.Backchannel;
using System.Web.Script.Serialization;
using UIC.Framweork.DefaultImplementation;
using UIC.Framework.Interfaces.Edm.Definition;
using HAW.AWS.CommunicationAgent.PropertiesFileReaders;

namespace HAW.AWS.CommunicationAgent 
{
    public class HAWCommunicationAgent : UIC.Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        private ISerializer serializer;
        private ILoggerFactory _loggerFactory;
        private ILogger _logger;
        private Action<Command> _commandHandler;
        private List<EmbeddedDriverModule> _edms;
        private PropertiesFileReader _propertyreader;

        public static string CONFIG_PATH ="config.properties";
        private static HAWCommunicationAgent _instance;
        private readonly Dictionary<Guid, CommandDefinition> _guidUicCommandMap = new Dictionary<Guid, CommandDefinition>();
        private readonly Dictionary<Guid, List<CommandDefinition>> _guidUicSensorCommandMap = new Dictionary<Guid, List<CommandDefinition>>();

        public HAWCommunicationAgent(ISerializer serializer, ILoggerFactory loggerFactory)
        {
            this.serializer = serializer;
            this._loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());
            _logger.Information("HAW Communication Agent built.");

            _propertyreader = new PropertiesFileReader(CONFIG_PATH);
            Thread RestServiceThread = new Thread(RestServer.startRESTService);
            RestServiceThread.Start();
            _instance = this;
        }

        public void Connect(Action<Command> commandHandler)
        {

         _commandHandler = commandHandler;
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

            _edms = edms;

            List<EDMWrapper> edmWrappers = new List<EDMWrapper>();
            foreach (EmbeddedDriverModule element in edms)
            {
                //building of ther maps
                BuildEdmMap(element.GetCapability());

                // edms must be converted in a actual type to serialize the capabilities
                edmWrappers.Add(new EDMWrapper(element));
            }





            RESTClient.RESTClient.Initialize(serialId, DataAndAttributeValueWrapper.GetJSON(edmWrappers), _logger);

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

        public static HAWCommunicationAgent getInstance()
        {
            return _instance;

        }

        public void handleCommand(UICRESTDataContract contract)
        {
            String commandStr = contract.payload;
            JavaScriptSerializer ser = new JavaScriptSerializer();
            CommandContract commandContract = ser.Deserialize<CommandContract>(contract.payload);

            foreach (EmbeddedDriverModule element in _edms)
            {
                if (element.Identifier.Id.Equals(new Guid(commandContract.id)))
                {
                    Console.WriteLine("[DEBUG] Found fitting EDM for command");
                    element.Handle(GetCommandFromContract(commandContract));
                }

            }


        }







        public Command GetCommandFromContract(CommandContract contract)
        {
            var id = new Guid(contract.commandId);
            var commandDefinition = _guidUicCommandMap[id];
            return new SgetCommand(commandDefinition, contract.commandPayload);
        }


        private void BuildEdmMap(EdmCapability edmCapability)
        {
            foreach (var command in edmCapability.CommandDefinitions)
            {
                _guidUicCommandMap.Add(command.Id, command);
                if (command.RelatedDatapoint != null)
                {
                    List<CommandDefinition> commandList;
                    if (_guidUicSensorCommandMap.TryGetValue(command.RelatedDatapoint.Id, out commandList))
                    {
                        commandList.Add(command);
                    }
                    else
                    {
                        _guidUicSensorCommandMap.Add(command.RelatedDatapoint.Id, new List<CommandDefinition> { command });
                    }
                }
            }
        }




    }
}
