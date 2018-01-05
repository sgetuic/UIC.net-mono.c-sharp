namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto
{
    public class CommandDto {
        public Identifier Identifier { get; set; }
        public string Name { get; set; }
        public string Command { get; set; }
        public string Label { get; set; }
        public Identifier DeviceTypeIdentifier { get; set; }

        public CommandMetadataViewModel Metadata { get; set; }
    }
}