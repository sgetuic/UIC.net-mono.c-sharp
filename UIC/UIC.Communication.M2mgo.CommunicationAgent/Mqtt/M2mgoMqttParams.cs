namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt
{
    internal class M2mgoMqttParams
    {
        public M2mgoMqttParams(string brokerUrl, bool deactivateSecureChannel = false)
        {
            BrokerUrl = brokerUrl;
            DeactivateSecureChannel = deactivateSecureChannel;
            BrokerPort = deactivateSecureChannel ? 1883 : 8883;
            
        }

        public string BrokerUrl { get; private set; }
        public bool DeactivateSecureChannel { get; private set; }
        public int BrokerPort { get; private set; }
    }
}