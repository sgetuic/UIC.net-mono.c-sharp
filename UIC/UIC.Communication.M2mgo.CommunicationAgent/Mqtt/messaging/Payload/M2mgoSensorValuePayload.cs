using System.Collections.Generic;
using System.Runtime.Serialization;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Payload
{
    internal class M2MgoSensorValuePayload : IPayload
    {
        private readonly string _payload;

        public M2MgoSensorValuePayload(IEnumerable<SensorValueDto> datapoints, ISerializer serializer)
        {
            _payload = serializer.Serialize(datapoints);
        }

        public M2MgoSensorValuePayload(SensorValueDto datapoints, ISerializer serializer) : this(new [] { datapoints }, serializer)
        {}

        public string GetPayload()
        {
            return _payload;
        }

        public class SensorValueDto
        {
            public SensorValueDto(string key, string value)
            {
                k = key;
                v = value;
            }

            public string k { get; set; }
            public string v { get; set; }
        }
    }
}