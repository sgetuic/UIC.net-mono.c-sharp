using HAW.AWS.CommunicationAgent.PropertiesFileReaders;
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
        public static string UIC_PORT = "port_uic";

        public static void startRESTService()
        {
            PropertiesFileReader propertyreader = new PropertiesFileReader(HAWCommunicationAgent.CONFIG_PATH);
            

            UICRestService DemoServices = new UICRestIMPL();
            WebServiceHost _serviceHost = new WebServiceHost(DemoServices,
            new Uri("http://localhost:" + propertyreader.getValue(UIC_PORT)));

            _serviceHost.Open();
            Console.ReadKey();
            _serviceHost.Close();
        }
    }
}
