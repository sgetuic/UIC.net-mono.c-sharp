using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint
{
    public class SGetCloudMapperDataPointDefinition
    {
        public SGetCloudMapperDataPointDefinition(string key, string name, string description, SGetCloudMapperDataType dataType, EmbeddedHwInterfaceIdentifier interfaceidentifier)
        {
            DataType = dataType;
            Description = description;
            Name = name;
            Key = key;
            InterfaceIdentifier = interfaceidentifier;
        }

        public string Key { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public EmbeddedHwInterfaceIdentifier InterfaceIdentifier { get; private set; }

        public SGetCloudMapperDataType DataType { get; private set; }
        public override string ToString()
        {
            return String.Format("{0} - {1} ({2})", Key, DataType, InterfaceIdentifier);
        }
    }
}