//-----------------------------------------------------------------------
// <copyright file="AzureProvisioningAgent.cs" company="Kontron Europe GmbH">
//     Copyright (c) Kontron Europe GmbH. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using UIC.Util.Logging;

namespace UIC.Communication.Azure.CommunicationAgent
{
    public class AzureProvisioningAgent
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;
        AzureHubConfiguration _azureHubConfiguration;

        public AzureProvisioningAgent(AzureHubConfiguration azureHubConfiguration, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());

            _azureHubConfiguration = azureHubConfiguration;
        }

        public bool TryGetDevice(string hwId, out string device_id)
        {
            // TODO: Attestation and Enrollment
            return _azureHubConfiguration.EdmMappings.TryGetValue(hwId, out device_id);
        }

    }
}
