using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute
{
    public class SGetCloudMapperAttributeDefinition
    {
        public SGetCloudMapperAttributeDefinition(string key, string label, string description, EmbeddedHwInterfaceIdentifier interfaceidentifier)
        {
            InterfaceIdentifier = interfaceidentifier;
            Label = label;
            Description = description;
            Key = key;
        }

        public string Key { get; private set; }
        public string Label { get; private set; }
        public string Description { get; private set; }
        public EmbeddedHwInterfaceIdentifier InterfaceIdentifier { get; private set; }

        public override string ToString()
        {
            return String.Format("{0} ({1})", Key, InterfaceIdentifier.ToString());
        }
    }
}