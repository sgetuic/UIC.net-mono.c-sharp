using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute
{
    public class SGetCloudMapperAttributeValue {
        private readonly SGetCloudMapperAttributeDefinition _definition;
        private readonly string _value;
        private readonly DateTime _ocurredTimeStamp;

        public SGetCloudMapperAttributeValue(SGetCloudMapperAttributeDefinition definition, string value)
        {
            _definition = definition;
            _value = value;
            _ocurredTimeStamp = DateTime.UtcNow;
        }

        public SGetCloudMapperAttributeValue(SGetCloudMapperAttributeDefinition definition, string value, DateTime ocurredTimeStamp): this(definition, value)
        {
            _ocurredTimeStamp = ocurredTimeStamp;
        }

        public string GetValue()
        {
            return _value;
        }
        
        public DateTime GetTimeStamp() {
            return _ocurredTimeStamp;
        }

        public SGetCloudMapperAttributeDefinition GetDefinition()
        {
            return _definition;
        }
    }
}