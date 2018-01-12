using UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.DeviceManagement
{
    internal class ApplianceBlueprints
    {
        public BlueprintDto GatewayBlueprint { get; private set; }
        public BlueprintDto ProjectBlueprint { get; private set; }
        
        public ApplianceBlueprints(BlueprintDto gatewayBlueprint, BlueprintDto projectBlueprint)
        {
            GatewayBlueprint = gatewayBlueprint;
            ProjectBlueprint = projectBlueprint;
        }
    }
}