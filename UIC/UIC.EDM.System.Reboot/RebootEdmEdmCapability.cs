using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdmEdmCapability : EdmCapability
    {
        public RebootEdmEdmCapability(EdmIdentifier getldentifier, CommandDefinition[] commandDefinitios, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Identifier = getldentifier;
            CommandDefinitions = commandDefinitios;
            AttributeDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }
        public EdmIdentifier Identifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttributeDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}