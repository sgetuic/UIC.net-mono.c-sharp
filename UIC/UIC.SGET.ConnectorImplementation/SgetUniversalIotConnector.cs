using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using UIC.Framework.Interfaces;
using UIC.Framework.Interfaces.Communication.Application;
using UIC.Framework.Interfaces.Communication.Projects;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.SGET.ConnectorImplementation.Monitoring;
using UIC.Util;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.SGET.ConnectorImplementation
{
    public class SgetUniversalIotConnector : UniversalIotConnector
    {
        private static readonly Guid BOARD_IDENTIFIER_DEFINITION_ID = new Guid("{a65a6538-96d1-4525-b0f2-5059dfa38e0e}");
        
        private readonly UicConfiguartion _uicConfiguartion;
        private readonly List<EmbeddedDriverModule> _embeddedDriverModules;
        private readonly CommunicationAgent _communicationAgent;
        private readonly ProjectAgent _projectAgent;
        private readonly ISerializer _serializer;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        private readonly Dictionary<Guid, EmbeddedDriverModule> _definitionEdmMap = new Dictionary<Guid, EmbeddedDriverModule>();
        private readonly List<DatapointMonitor> _datapointMonitors = new List<DatapointMonitor>();
        

        public SgetUniversalIotConnector(
            UicConfiguartion uicConfiguartion, 
            List<EmbeddedDriverModule> embeddedDriverModules,
            CommunicationAgent communicationAgent, 
            ProjectAgent projectAgent, 
            ISerializer serializer, 
            ILoggerFactory loggerFactory) {
            _uicConfiguartion = uicConfiguartion;
            _embeddedDriverModules = embeddedDriverModules;
            _communicationAgent = communicationAgent;
            _projectAgent = projectAgent;
            _serializer = serializer;
            _loggerFactory = loggerFactory;

            _logger = _loggerFactory.GetLoggerFor(GetType());
        }

        public string SerialId { get; private set; }

        public void Initialize()
        {
            _logger.Information("Initialize");
            foreach (EmbeddedDriverModule edm in _embeddedDriverModules)
            {
                edm.Initialize();
                EdmCapability edmCapability = edm.GetCapability();
                BuildEdmMap(edm, edmCapability);
               
            }

            _projectAgent.Initialize(_embeddedDriverModules.ToArray());

            if (_uicConfiguartion.IsEdmSnychronizationEnabled)
            {
                foreach (EmbeddedDriverModule edm in _embeddedDriverModules)
                {
                    EdmCapability edmCapability = edm.GetCapability();
                    _projectAgent.Synchronize(edm.GetCapability());
                }
                
            }



            UicProject project = LoadUicProject();

            SerialId = GernerateApplianceSerialKey(project);

            _communicationAgent.Initialize(SerialId, project, _embeddedDriverModules);
            _communicationAgent.Connect(CloudAgentCommandHandler);

            PushAttributeValues(project);
            
            StartDatapointMonitoring(project);
        }

        private string GernerateApplianceSerialKey(UicProject project) {
            string serialKey;
            var eapiEdm = GetEdmFor(BOARD_IDENTIFIER_DEFINITION_ID);
            if (eapiEdm == null) {
                serialKey = Environment.MachineName;
            }
            else {
                serialKey = eapiEdm.GetValueFor(eapiEdm.GetCapability().AttributeDefinitions.Single(a => a.Id == BOARD_IDENTIFIER_DEFINITION_ID)).Value.ToString();
            }

            return serialKey + "." + project.ProjectKey;
        }

        private void StartDatapointMonitoring(UicProject project) {
            var dataPointEvaluatorProvider = new DataPointEvaluatorProvider();
            foreach (ProjectDatapointTask datapointTask in project.DatapointTasks) {
                try
                {
                    EmbeddedDriverModule edm = GetEdmFor(datapointTask.Definition.Id);

                    _datapointMonitors.Add(new DatapointMonitor(datapointTask, dataPointEvaluatorProvider, this, _loggerFactory.GetLoggerFor("DatapointMonitor-"+datapointTask.Definition.Label)));
                    
                    //Set Asynchronous EDM Calbbacks if necessary
                    edm.SetDatapointCallback(datapointTask, datapointValue => new Thread(() => _communicationAgent.Push(datapointValue)).Start());
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Could not start datapoint monitoring for : " + datapointTask);
                }
            }
        }

        private void PushAttributeValues(UicProject project) {
            // read and publish 
            foreach (var attribtueDefinition in project.Attributes) {
                try
                {
                    EmbeddedDriverModule edm = GetEdmFor(attribtueDefinition.Id);
                    AttributeValue attibutValue = edm.GetValueFor(attribtueDefinition);
                    _communicationAgent.Push(attibutValue);

                    //Set Asynchronous EDM Calbbacks if necessary
                    edm.SetAttributeCallback(attribtueDefinition, attributeValue => _communicationAgent.Push(attributeValue));
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Could not push attribute value for: " + attribtueDefinition);
                }
            }
        }

        private void BuildEdmMap(EmbeddedDriverModule edm, EdmCapability edmCapability) {
            foreach (var definition in edmCapability.AttributeDefinitions) {
                _definitionEdmMap.Add(definition.Id, edm);
            }

            foreach (var definition in edmCapability.DatapointDefinitions) {
                _definitionEdmMap.Add(definition.Id, edm);
            }

            foreach (var definition in edmCapability.CommandDefinitions) {
                _definitionEdmMap.Add(definition.Id, edm);
            }
        }

        private UicProject LoadUicProject() {
            UicProject project;
            var serializedProjectFilepath = _uicConfiguartion.ProjectJsonFilePath;
            var jsonFileHandler = new ConfigurationJsonFileHandler(serializedProjectFilepath, _serializer, _logger);
            
            if (_uicConfiguartion.IsRemoteProjectLoadingEnabled) {
                project = _projectAgent.LoadProject(_uicConfiguartion);
                jsonFileHandler.Backup(project);
            }
            else {
                project = jsonFileHandler.Load<UicProject>();
            }

            if (project == null) throw new ApplicationException("no project could be loaded");
            return project;
        }

        private void CloudAgentCommandHandler(Command command)
        {
            EmbeddedDriverModule edm = GetEdmFor(command.CommandDefinition.RelatedDatapoint.Id);
            edm.Handle(command);
        }


        public EmbeddedDriverModule GetEdmFor(Guid definitionId)
        {
            EmbeddedDriverModule edm;
            if (_definitionEdmMap.TryGetValue(definitionId, out edm)) {
                return edm;
            }
            return null;
        }

        public void Push(DatapointValue val) {
            _communicationAgent.Push(val);
        }

        public void Dispose()
        {
            try
            {
                foreach (var monitor in _datapointMonitors) {
                    try
                    {
                        _logger.Information("Dispose monitor " + monitor);
                        monitor.Dispose();
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e, "Dispose monitor " + monitor);
                    }
                }
            }
            catch (Exception e)
            {
                _logger.Error(e, "Dispose _communicationAgent " + _communicationAgent);
            }

            try
            {
                _logger.Information("Dispose _communicationAgent " + _communicationAgent);
                _communicationAgent.Dispose();
            }
            catch (Exception e)
            {
                _logger.Error(e, "Dispose _communicationAgent " + _communicationAgent);
            }

            foreach (var edm in _embeddedDriverModules) {
                try
                {
                    _logger.Information("Dispose EDM " + edm.Identifier);
                    edm.Dispose();
                }
                catch (Exception e)
                {
                    _logger.Error(e, "Dispose EDM " + edm.Identifier);

                }
            }
        }
    }
}
