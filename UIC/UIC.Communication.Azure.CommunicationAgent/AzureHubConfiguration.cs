//-----------------------------------------------------------------------
// <copyright file="AzureHubConfiguration.cs" company="Kontron Europe GmbH">
//     Copyright (c) Kontron Europe GmbH. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System.Collections.Generic;
using UIC.Util.Serialization;

namespace UIC.Communication.Azure.CommunicationAgent
{
    public class AzureHubConfiguration
    {
        public string IotHubUri { get; private set; }
        public Dictionary<string, string> DeviceKeys { get; private set; }
        public Dictionary<string, string> EdmMappings { get; private set; }

        public AzureHubConfiguration(string iotHubUri, Dictionary<string, string> deviceKeys, Dictionary<string, string> edmMappings)
        {
            IotHubUri = iotHubUri;
            DeviceKeys = deviceKeys;
            EdmMappings = edmMappings;
        }

        private static string pstConfig = @"
{
  ""IotHubUri"": ""SGET-UIC-IotHub.azure-devices.net"",
  ""DeviceKeys"": {
    ""uic_device_mockup"": ""u2oby8L4cEG9ivBZzAN1L1OYZs1uGv6SknbGGfFsHp8="",
    ""uic_device_eapi"": ""NPzFCxMyQPPehQVN8LoumZ5qxIKnkwr2+jcspX6yGcI=""
  },
  ""EdmMappings"": {
    ""1e7b861c-d6a0-4993-812e-0ffd42a4c77d"": ""uic_device_mockup"",
    ""b3bb9ffa-43ba-49f9-bbb2-40d1de291215"": ""uic_device_eapi"",
    ""d958bb24-7a1a-4cb6-bb7f-3a5868d3170d"": ""uic_device_eapi""
  }
}
        ";

        public static AzureHubConfiguration PstConfig(ISerializer serializer)
        {
            return serializer.Deserialize<AzureHubConfiguration>(pstConfig);

            /*
                        Dictionary<string, string> deviceKeys = new Dictionary<string, string>();
                        deviceKeys.Add("uic_device_mockup", "u2oby8L4cEG9ivBZzAN1L1OYZs1uGv6SknbGGfFsHp8=");
                        deviceKeys.Add("uic_device_eapi", "NPzFCxMyQPPehQVN8LoumZ5qxIKnkwr2+jcspX6yGcI=");


                        Dictionary<string, string> edmMappings = new Dictionary<string, string>();
                        edmMappings.Add("1e7b861c-d6a0-4993-812e-0ffd42a4c77d", "uic_device_mockup");
                        edmMappings.Add("b3bb9ffa-43ba-49f9-bbb2-40d1de291215", "uic_device_eapi");
                        edmMappings.Add("d958bb24-7a1a-4cb6-bb7f-3a5868d3170d", "uic_device_eapi");

                        return new AzureHubConfiguration("SGET-UIC-IotHub.azure-devices.net", deviceKeys, edmMappings);
            */
        }
    }
}
