//-----------------------------------------------------------------------
// <copyright file="AzureCommunicationAgentImpl.cs" company="Kontron Europe GmbH">
//     Copyright (c) Kontron Europe GmbH. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using Microsoft.Azure.Devices.Client;
using UIC.Util.Logging;
using UIC.Util.Serialization;
using UIC.Util;
using Microsoft.Azure.Devices.Shared;

namespace UIC.Communication.Azure.CommunicationAgent
{
    public class AzureCommunicationAgentImpl : Framework.Interfaces.Communication.Application.CommunicationAgent
    {
        private readonly ISerializer _serializer;
        private readonly ILoggerFactory _loggerFactory;
        private readonly ILogger _logger;

        /* Azure device client */
        private Dictionary<string, DeviceClient> _deviceClients;
        private AzureHubConfiguration _hubConfiguration;
        private Dictionary<Guid, string> _datapointMappings = new Dictionary<Guid, string>();
        private Dictionary<Guid, string> _attributeMappings = new Dictionary<Guid, string>();


        public AzureCommunicationAgentImpl(ISerializer serializer, ILoggerFactory loggerFactory)
        {
            _serializer = serializer;
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());

            _hubConfiguration = GetConfiguration(serializer, _logger);
        }

        private AzureHubConfiguration GetConfiguration(ISerializer serializer, ILogger _logger)
        {
            var configHandler = new ConfigurationJsonFileHandler(@".\azure_communication_agent.json", serializer, _logger);
            if (configHandler.IsConfigFileExisting())
            {
                return configHandler.Load<AzureHubConfiguration>();
            }
            var config = AzureHubConfiguration.PstConfig(serializer);
            configHandler.Backup(config);
            return config;
        }

        public void Connect(Action<Command> commandHandler)
        {
            _deviceClients = _hubConfiguration.DeviceKeys.ToDictionary(
                kvp => kvp.Key,
                kvp => DeviceClient.Create(
                                _hubConfiguration.IotHubUri,
                                new DeviceAuthenticationWithRegistrySymmetricKey(kvp.Key, kvp.Value),
                                TransportType.Mqtt)
                             );
        }

        public void Dispose()
        {
            foreach (var c in _deviceClients.Values)
            {
                c.CloseAsync().Wait();
            }
        }

        public void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms)
        {
            AzureProvisioningAgent provisioningAgent = new AzureProvisioningAgent(_hubConfiguration, _loggerFactory);
            foreach (var edm in edms)
            {
                string device_id;
                if (provisioningAgent.TryGetDevice(edm.Identifier.Id.ToString(), out device_id))
                {
                    _logger.Debug("linking datapoints and attributes from EDM {0} to device {1}",
                        edm.Identifier.Uri, device_id);
                    foreach (var cap in edm.GetCapability().DatapointDefinitions)
                    {
                        _datapointMappings.Add(cap.Id, device_id);
                    }
                    foreach (var cap in edm.GetCapability().AttributeDefinitions)
                    {
                        _attributeMappings.Add(cap.Id, device_id);
                    }
                }
                else
                {
                    _logger.Warning("EDM provisioning failed for {0}:{1}",
                        edm.Identifier.Uri, edm.Identifier.Id);
                }
            }
        }

        public void Push(DatapointValue value)
        {
            string client_id;
            if (_datapointMappings.TryGetValue(value.Definition.Id, out client_id))
            {
                var messageData = new
                {
                    Id = value.Definition.Id,
                    Label = value.Definition.Label,
                    Value = value.Value.ToString()
                };
                var messageString = _serializer.Serialize(messageData);
                _deviceClients[client_id].SendEventAsync(new Message(Encoding.ASCII.GetBytes(messageString)));
                _logger.Debug("DataPoint push at {0} : '{1}'", DateTime.Now, messageString);
            }
            else
            {
                _logger.Warning("no datapoint mapping found for {0}", value.Definition.Id);
            }
        }

        public void Push(IEnumerable<DatapointValue> values)
        {
            foreach (var v in values)
            {
                Push(v);
            }
        }

        private async Task reportAttribute(DeviceClient client, AttributeValue value)
        {
            Twin twin = await client.GetTwinAsync();
            twin.Properties.Reported.ClearMetadata();
            twin.Properties.Reported[value.Definition.Label] = value.Value.ToString();
            await client.UpdateReportedPropertiesAsync(twin.Properties.Reported);
        }

        public void Push(AttributeValue value)
        {
            string client_id;
            if (_attributeMappings.TryGetValue(value.Definition.Id, out client_id))
            {
                reportAttribute(_deviceClients[client_id], value).Wait();
                _logger.Debug("Attribute push at {0} : '{1}'", DateTime.Now, value.Definition.Label);
            }
            else
            {
                _logger.Warning("no attribute mapping found for {0}:{1}", value.Definition.Id, value.Definition.Label);
            }
        }

        public void Push(IEnumerable<AttributeValue> values)
        {
            foreach (var v in values)
            {
                Push(v);
            }
        }

        public void Debug(string debug)
        {
            _logger.Debug(debug);
        }
    }
}
