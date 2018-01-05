using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic
{
    internal class DataTopic : ITopic
    {
        private readonly Guid _blueprintId;
        private readonly string _thingId;
        private readonly string _sensorId;

        public DataTopic(Guid blueprintId, string thingId, string sensorId = null) {
            _blueprintId = blueprintId;
            _thingId = thingId;
            _sensorId = sensorId;
        }

        public string GetTopic() {
            return M2MgoTopics.GetDataTopic(_blueprintId, _thingId, _sensorId);
        }
    }
}