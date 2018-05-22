using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace HAW.AWS.CommunicationAgent.RESTClient
{
    class RestServer
    {
        public static void startRESTService()
        {
            UICRestService DemoServices = new UICRestIMPL();
            WebServiceHost _serviceHost = new WebServiceHost(DemoServices,
            new Uri("http://localhost:8081/"));

            _serviceHost.Open();
            Console.ReadKey();
            _serviceHost.Close();
        }
    }
}
