using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.Project
{
    internal class M2mgoGetwayProjectDto
    {
        public GatewayGetModel Gateway { get; private set; }
        public GatewayProjectGetModel GatewayProject { get; private set; }

        public M2mgoGetwayProjectDto(GatewayGetModel gateway, GatewayProjectGetModel gatewayProject)
        {
            Gateway = gateway;
            GatewayProject = gatewayProject;
        }
    }
}