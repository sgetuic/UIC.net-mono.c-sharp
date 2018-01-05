using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway {
    internal class GatewayProjectGetModel {
        
        public string Name { get; set; }
        public string Label { get; set; }
        public Guid ID { get; set; }
        public Identifier Domain { get; set; }
    }
}
