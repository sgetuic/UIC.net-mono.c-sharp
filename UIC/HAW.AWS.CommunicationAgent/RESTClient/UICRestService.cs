using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HAW.AWS.CommunicationAgent.RESTClient
{
    [ServiceContract(Name = "RESTDemoServices")]
    interface UICRestService
    {
        [WebInvoke(Method = "POST", UriTemplate = Routing.GetClientRoute, BodyStyle = WebMessageBodyStyle.Bare,
         RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        [OperationContract]
        string pushEDMActionData(UICRESTDataContract JSONdata );
    }
}
