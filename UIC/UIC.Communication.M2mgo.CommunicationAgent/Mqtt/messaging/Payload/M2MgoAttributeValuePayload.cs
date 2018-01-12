using System.Collections.Generic;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Payload
{
    internal class M2MgoAttributeValuePayload : IPayload
    {
        private string _payload;

        public M2MgoAttributeValuePayload(IEnumerable<AttributeValueDto> attributes, ISerializer serializer)
        {
            _payload = serializer.Serialize(attributes);
        }

        public string GetPayload()
        {
            return _payload;
        }

        public class AttributeValueDto
        {
            public AttributeValueDto(string key, string value)
            {
                k = key;
                v = value;
            }

            public string k { get; set; }
            public string v { get; set; }
        }
    }
}
