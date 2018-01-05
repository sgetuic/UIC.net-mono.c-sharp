namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto
{
    public class Attribute {
        public Identifier Identifier { get; set; }
        public string Name { get; set; }
        public string Label { get; set; }
        public object Description { get; set; }
        public object Icon { get; set; }
        public Identifier DeviceTypeIdentifier { get; set; }
    }
}