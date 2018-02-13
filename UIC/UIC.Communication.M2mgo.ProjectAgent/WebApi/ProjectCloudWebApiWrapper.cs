using System;
using System.Linq;
using System.Net;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi
{
    class ProjectCloudWebApiWrapper
    {
        private readonly ILogger _logger;
        private readonly ISerializer _serializer;

        public ProjectCloudWebApiWrapper(ILogger logger, ISerializer serializer)
        {
            _logger = logger;
            _serializer = serializer;
        }

        public void SynchronizeEmbeddedModuleFunctions(M2mgoProjectAgentConfiguration configuration, SgetEmbeddedDriverModuleAppliance sgetEmbeddedDriverModuleAppliance)
        {
            if (sgetEmbeddedDriverModuleAppliance.EmbeddedDriverModules.Any(edm => edm.Identifier == null))
            {
                throw new Exception("edm needs an identifier for the project cloud");
            }
            string postData = _serializer.Serialize(sgetEmbeddedDriverModuleAppliance);
            _logger.Information(postData);

            string url = configuration.EdmSnychronizationUrl;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            new WebApiRequestExecutor().ExecuteRequest(request, postData, _logger);
        }


        public SgetProject LoadProjectConfiguration(M2mgoProjectAgentConfiguration configuration, string projectKey)
        {
            _logger.Information("LoadProjectConfiguration for " + projectKey);
            string url = configuration.RemoteProjectConfigurationUrl.TrimEnd('/') + "/" + projectKey;
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            string rawData =  new WebApiRequestExecutor().ExecuteRequest(request, string.Empty, _logger);
            var sgetProject = _serializer.Deserialize<SgetProject>(rawData);
            return sgetProject;
        }
    }
}
