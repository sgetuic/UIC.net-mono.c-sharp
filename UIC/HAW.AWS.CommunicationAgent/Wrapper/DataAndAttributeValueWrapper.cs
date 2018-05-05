using UIC.Framework.Interfaces.Edm.Value;
using System.Web.Script.Serialization;

namespace HAW.AWS.CommunicationAgent.Wrapper
{
    static class DataAndAttributeValueWrapper
    {
        private static JavaScriptSerializer JSerial = new JavaScriptSerializer();

        public static string GetJSON(object input)
        {
            return JSerial.Serialize(input);
        }
            
    }
}
