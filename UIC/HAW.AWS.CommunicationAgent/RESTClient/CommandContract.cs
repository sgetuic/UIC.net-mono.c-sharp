using System.Runtime.Serialization;


namespace HAW.AWS.CommunicationAgent.Backchannel
{
    [DataContract]
    public class CommandContract
    {
    
       
            [DataMember]
            public string id { get; set; }
            [DataMember]
            public string commandId { get; set; }
            [DataMember]
            public string commandPayload { get; set; }
    }
}

