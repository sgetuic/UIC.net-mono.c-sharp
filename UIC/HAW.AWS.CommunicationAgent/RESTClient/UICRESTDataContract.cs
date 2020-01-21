using System.Runtime.Serialization;


namespace HAW.AWS.CommunicationAgent.RESTClient
{
    [DataContract]
    public class UICRESTDataContract
    {
    
       
            [DataMember]
            public string topic { get; set; }
            [DataMember]
            public string payload { get; set; }

    }
 }

