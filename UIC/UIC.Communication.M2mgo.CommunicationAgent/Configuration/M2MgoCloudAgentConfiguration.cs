using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.Configuration
{
    public class M2MgoCloudAgentConfiguration
    {
        public string BaseUrl { get; set; }
        public string BrokerBaseUrl { get; set; }
        public Guid SgetGatewayTypeId { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
        public Guid SgetDomainID { get; set; }


        internal static M2MgoCloudAgentConfiguration LocalhostConfig() {
            return new M2MgoCloudAgentConfiguration {
                BaseUrl = "http://localhost:2796/",
                BrokerBaseUrl = "broker-development.m2mgo.com",
                SgetGatewayTypeId = new Guid("f5281d80-0528-4dc8-b297-fd7efd164b65"),
                User = "gateway.agent@m2mgo.com",
                Password = "sgetsget",
                SgetDomainID = new Guid("{12a52d5d-6ce5-407d-b557-26dd44c9ae9c}"),
            };
        }
        
        internal static M2MgoCloudAgentConfiguration PstConfig() {
            return new M2MgoCloudAgentConfiguration {
                BaseUrl = "https://pst.m2mgo.com/",
                BrokerBaseUrl = "broker-pst.m2mgo.com",
                SgetGatewayTypeId = new Guid("f5281d80-0528-4dc8-b297-fd7efd164b65"),
                User = "gateway.agent@m2mgo.com",
                Password = "sgetsget",
                SgetDomainID = new Guid("{12a52d5d-6ce5-407d-b557-26dd44c9ae9c}"),
            };
        }
    }
}
