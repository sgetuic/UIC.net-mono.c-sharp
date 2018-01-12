using System;
using System.Collections.Generic;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway {
    internal class GatewayGetModel {

        public Identifier Identifier { get; set; }

        public Identifier DomainIdentifier { get; set; }

        public GatewayProjectGetModel GatewayType { get; set; }

        public string Label { get; set; }
        public string Serial { get; set; }
        public GatewayStatus Status { get; set; }
        public DateTime Timestamp { get; set; }
        public IEnumerable<RelevantDevice> RelevantDevices { get; set; }

        public Identifier GatewayProjectIdentifier { get; set; }
    }
}
