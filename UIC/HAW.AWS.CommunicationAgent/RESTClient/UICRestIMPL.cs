using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;

using System.Text;
using System.Threading.Tasks;

namespace HAW.AWS.CommunicationAgent.RESTClient
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, ConcurrencyMode = ConcurrencyMode.Single, IncludeExceptionDetailInFaults = true)]
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    class UICRestIMPL : UICRestService
    {
        public string pushEDMActionData(UICRESTDataContract JSONdata)
        {
            HAWCommunicationAgent.getInstance().handleCommand(JSONdata);
            return JSONdata.payload +"topic: " + JSONdata.topic;
        }          
    }
}
