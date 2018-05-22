using UIC.Framework.Interfaces.Edm.Value;
using System.Web.Script.Serialization;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace HAW.AWS.CommunicationAgent.Wrapper
{
    class EDMWrapper
    {
        public EdmIdentifier identifier { get; }
        public EdmCapability capability { get; }

public EDMWrapper(UIC.Framework.Interfaces.Edm.EmbeddedDriverModule element)
        {
            identifier = element.Identifier;
            capability = element.GetCapability();
        }
    }
}
