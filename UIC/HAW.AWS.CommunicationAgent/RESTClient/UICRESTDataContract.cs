using System.Runtime.Serialization;


namespace HAW.AWS.CommunicationAgent.RESTClient
{
    [DataContract]
    public class UICRESTDataContract
    {
    
       
            [DataMember]
            public string OrderID { get; set; }

        
        }
    }

