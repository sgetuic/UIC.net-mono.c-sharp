using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Payload;
using UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging {
    internal class M2MgoPublishMessage {
        private readonly ITopic _topic;
        private readonly IPayload _payload;

        public M2MgoPublishMessage(ITopic topic, IPayload payload) {
            _topic = topic;
            _payload = payload;
        }

        public string GetPayload() {
            return _payload.GetPayload();
        }

        public string GetTopic() {
            return _topic.GetTopic();
        }
    }
}
