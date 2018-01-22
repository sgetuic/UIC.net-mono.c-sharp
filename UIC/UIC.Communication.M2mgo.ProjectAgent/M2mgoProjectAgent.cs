using System.Collections.Generic;
using UIC.Communication.M2mgo.ProjectAgent.WebApi;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Util;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.ProjectAgent
{
    public class M2mgoProjectAgent : Framework.Interfaces.Communication.Projects.ProjectAgent
    {
        private readonly ILogger _logger;
        private readonly ProjectCloudWebApiWrapper _projectCloudWebApiWrapper;
        private readonly M2mgoProjectAgentConfiguration _configuration;
        private readonly SgetUicProjectTranlator _projectTranslator;

        public M2mgoProjectAgent(ISerializer serializer, ILoggerFactory loggerFactory, List<EmbeddedDriverModule> modules) {

            _logger = loggerFactory.GetLoggerFor(GetType());
            _configuration = GetConfiguration(serializer);
        
            _projectCloudWebApiWrapper = new ProjectCloudWebApiWrapper(loggerFactory.GetLoggerFor(typeof(ProjectCloudWebApiWrapper)), serializer);
            _projectTranslator = new SgetUicProjectTranlator(modules, loggerFactory.GetLoggerFor(typeof(SgetUicProjectTranlator)));
        }
        
        
        public void Synchronize(EdmCapability edmCapability) {
            SgetEmbeddedDriverModuleAppliance appliance = _projectTranslator.Translate(edmCapability);
            _projectCloudWebApiWrapper.SynchronizeEmbeddedModuleFunctions(_configuration, appliance);
        }

        public UicProject LoadProject(UicConfiguartion uicConfiguartion) {
            var sgetProject = _projectCloudWebApiWrapper.LoadProjectConfiguration(_configuration, uicConfiguartion.ProjectKey);
            var uicproject = _projectTranslator.Translate(sgetProject);
            return uicproject;
        }

        private M2mgoProjectAgentConfiguration GetConfiguration(ISerializer serializer)
        {
            var configHandler = new ConfigurationJsonFileHandler(@".\projectagent.json", serializer, _logger);
            M2mgoProjectAgentConfiguration config;
            if (configHandler.IsConfigFileExisting())
            {
                config = configHandler.Load<M2mgoProjectAgentConfiguration>();
            }
            else
            {
                //config = new M2mgoProjectAgentConfiguration {
                //    RemoteProjectConfigurationUrl =  "https://pst.m2mgo.com/api/sget/project/board/",
                //    EdmSnychronizationUrl =  "https://pst.m2mgo.com/api/sget/embedded-modules/synch/"

                //};
                config = new M2mgoProjectAgentConfiguration {
                    RemoteProjectConfigurationUrl =  "https://pst.m2mgo.com/api/sget/project/board/",
                    EdmSnychronizationUrl =  "https://development.m2mgo.com/api/sget/embedded-modules/synch/"

                };
                configHandler.Backup(config);
            }
            return config;
        }
    }
}
