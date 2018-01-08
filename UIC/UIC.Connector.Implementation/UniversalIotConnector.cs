using System;
using UIC.Framework.Interfaces.Communication.Application;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Util;


namespace UIC.Connector.Implementation
{
    class UniversalIotConnector :IDisposable
    {
        private UicConfiguartionProvider uicConfiguartionProvider;
        private UicConfiguartion _uicConfiguartion;
        private CommunicationAgent _communicationAgent;

        public void Startup()
        {
            _uicConfiguartion = uicConfiguartionProvider.GetUicConfiguartion();

            string serialId = "extract serial Id of the embedded device as asset identifier for the cloud applicaiton";

            foreach (EmbeddedDriverModule edm in _uicConfiguartion.EmbeddedDriverModulesToLoad)
            {
                edm.Initialize();
            }

            if (_uicConfiguartion.IsEdmSnychronizationEnabled)
            {
                foreach (EmbeddedDriverModule edm in _uicConfiguartion.EmbeddedDriverModulesToLoad)
                {
                    EdmCapability edmCapability = edm.GetCapability();
                    // Synchronize edm with edm and project cloud
                }
            }

            UicProject project = null;
            if (_uicConfiguartion.IsRemoteProjectLoadingEnabled)
            {
                //Url EdmSnychronizationUrl { get; }
                //Url RemoteProjectConfigurationUrl();

                //var projectConfigurationUrl = _uicConfiguartion.RemoteProjectConfigurationUrl();
                //project = LoadProjectFromRemote(projectConfigurationUrl);
            }
            else
            {
                var serializedProjectFilepath = _uicConfiguartion.AbsoluteProjectConfigurationFilePath;
                project = LoadProjectFromFile(serializedProjectFilepath);
            }
            
            _communicationAgent.Synchronize(serialId, project);
            _communicationAgent.Connect(CloudAgentCommandHandler);

            // read and publish 
            foreach (var attribtueDefinition in project.Attributes)
            {
                EmbeddedDriverModule edm = GetEdmFor(attribtueDefinition);
                AttributeValue attibutValue = edm.GetValueFor(attribtueDefinition);
                _communicationAgent.Push(attibutValue);

                //Set Asynchronous EDM Calbbacks if necessary
                edm.SetAttributeCallback(attribtueDefinition, attributeValue => _communicationAgent.Push(attributeValue));
            }

            foreach (ProjectDatapointTask datapointTask in project.DatapointTasks)
            {
                EmbeddedDriverModule edm = GetEdmFor(datapointTask.Definition);

                //start Monitoring the DatapointTasks from the project
                StartDatapointMonitoring(datapointTask.Definition, datapointTask.PollIntervall,
                    datapointTask.ReportingCondition, edm);

                //Set Asynchronous EDM Calbbacks if necessary
                edm.SetDatapointCallback(datapointTask, datapointValue => _communicationAgent.Push(datapointValue));
            }
        }

        private void CloudAgentCommandHandler(Command command)
        {
            EmbeddedDriverModule edm = GetEdmFor(command);
            edm.Handle(command);
        }
        
        public void Dispose()
        {
            // Dispose Datapoint Monitoring

            // Dispose Cloud Agent

            // Dispose Embedded Driver Modules
            
        }

        private EmbeddedDriverModule GetEdmFor(AttributeDefinition attributeDefinition)
        {
            throw new NotImplementedException();
        }
        private EmbeddedDriverModule GetEdmFor(DatapointDefinition definition)
        {
            throw new NotImplementedException();
        }

        private EmbeddedDriverModule GetEdmFor(Command command)
        {
            throw new NotImplementedException();
        }

        private UicProject LoadProjectFromRemote(Url projectConfigurationUrl)
        {
            throw new NotImplementedException();
        }

        private UicProject LoadProjectFromFile(string serializedProjectFilepath)
        {
            throw new NotImplementedException();
        }

        private void StartDatapointMonitoring(DatapointDefinition datapointTaskDefinition, long datapointTaskPollIntervall, DatapointTaskReportingCondition datapointTaskReportingCondition, EmbeddedDriverModule edm)
        {
            throw new NotImplementedException();
        }


    }
}
