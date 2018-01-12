using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic
{
    public class AttributeTopic : ITopic
    {
        private readonly Guid _blueprintId;
        private readonly string _thingId;

        public AttributeTopic(Guid blueprintId, string thingId) {
            _blueprintId = blueprintId;
            _thingId = thingId;
        }

        public string GetTopic() {
            return M2MgoTopics.GetAttributeTopic(_blueprintId, _thingId);
        }
    }
}