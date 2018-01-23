using UIC.Util;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.Configuration
{
    internal static class ConfigurationProvider
    {
        public static M2MgoCloudAgentConfiguration GetConfiguration(ISerializer serializer, ILogger _logger) {
            var configHandler = new ConfigurationJsonFileHandler(@".\m2mgo_communication_agnet.json", serializer, _logger);
            if (configHandler.IsConfigFileExisting()) {
                return configHandler.Load<M2MgoCloudAgentConfiguration>();
            }
            var config = M2MgoCloudAgentConfiguration.PstConfig();
            configHandler.Backup(config);
            return config;
        }
    }
}