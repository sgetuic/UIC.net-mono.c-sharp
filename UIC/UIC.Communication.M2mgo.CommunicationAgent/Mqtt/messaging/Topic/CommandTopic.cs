using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic
{
    internal class CommandTopic : ITopic
    {
        private readonly Guid _blueprintId;
        private readonly string _thingId;

        public CommandTopic(Guid blueprintId, string thingId) {
            _blueprintId = blueprintId;
            _thingId = thingId;
        }

        public string GetTopic() {
            return M2MgoTopics.GetCommandTopic(_blueprintId, _thingId);
        }
    }
}