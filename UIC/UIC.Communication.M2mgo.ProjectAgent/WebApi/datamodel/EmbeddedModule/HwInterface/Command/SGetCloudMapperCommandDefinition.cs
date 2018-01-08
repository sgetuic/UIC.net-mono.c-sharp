using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Command
{
    public class SGetCloudMapperCommandDefinition
    {
        public SGetCloudMapperCommandDefinition(string name, string command, EmbeddedHwInterfaceIdentifier interfaceIdentifier, string[] tags, SGetCloudMapperDataPointDefinition relatedSensorKey)
        {
            RelatedSensorKey = relatedSensorKey;
            Tags = tags;
            Command = command;
            InterfaceIdentifier = interfaceIdentifier;
            Name = name;
        }

        public string Name { get; private set; }
        public string Command { get; private set; }

        public string[] Tags { get; private set; }
        public SGetCloudMapperDataPointDefinition RelatedSensorKey { get; private set; }

        public EmbeddedHwInterfaceIdentifier InterfaceIdentifier { get; private set; }
    }
}