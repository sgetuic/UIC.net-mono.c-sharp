using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway {
    public class GatewayProjectPutModel {
        public Guid DomainID { get; set; }
        public Guid GatewayTypeID { get; set; }
        public string Name { get; set; }
        public string ForeignSystemKey { get; set; }
        public string Configuration { get; set; }
        public string ConfigurationType { get; set; }

        public GatewayProjectPutModel() { }

        public GatewayProjectPutModel(Guid domainID, Guid gatewayTypeID, string name, string foreignSystemKey, string configuration, string configurationType) {
            DomainID = domainID;
            GatewayTypeID = gatewayTypeID;
            Name = name;
            ForeignSystemKey = foreignSystemKey;
            Configuration = configuration;
            ConfigurationType = configurationType;
        }

    }
}
