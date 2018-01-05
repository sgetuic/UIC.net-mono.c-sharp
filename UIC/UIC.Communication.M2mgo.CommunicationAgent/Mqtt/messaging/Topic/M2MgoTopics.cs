using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt.messaging.Topic
{
    internal class M2MgoTopics
    {
        private const string BlueprintToken = "{BlueprintID}";
        private const string CustomThingIdToken = "{CustomThingID}";
        private const string DataTopicTemplate = "m2mgo/" + BlueprintToken + "/" + CustomThingIdToken + "/data";
        private const string CommandTopicTemplate = "m2mgo/" + BlueprintToken + "/" + CustomThingIdToken + "/command";
        private const string AttributeTopicTemplate = "m2mgo/" + BlueprintToken + "/" + CustomThingIdToken + "/attribute";
        private const string LocationTopicTemplate = "m2mgo/" + BlueprintToken + "/" + CustomThingIdToken + "/data";

        internal static string GetDataTopic(Guid blueprintId, string customThingId, string sensorId) {
            var topic = DataTopicTemplate
                .Replace(BlueprintToken, blueprintId.ToString("D"))
                .Replace(CustomThingIdToken, customThingId);
            if (!String.IsNullOrWhiteSpace(sensorId)) {
                topic = topic + "/" + sensorId;
            }
            return topic;
        }

        internal static string GetAttributeTopic(Guid blueprintId, string customThingId) {
            return AttributeTopicTemplate
                .Replace(BlueprintToken, blueprintId.ToString("D"))
                .Replace(CustomThingIdToken, customThingId);
        }

        public static string GetCommandTopic(Guid blueprintId, string customThingId) {
            return CommandTopicTemplate
                .Replace(BlueprintToken, blueprintId.ToString("D"))
                .Replace(CustomThingIdToken, customThingId);
        }

        public static string GetLocationTopic(Guid blueprintId, string customThingId, string sensorId) {
            return LocationTopicTemplate
                .Replace(BlueprintToken, blueprintId.ToString("D"))
                .Replace(CustomThingIdToken, customThingId) + sensorId;
        }
    }
}