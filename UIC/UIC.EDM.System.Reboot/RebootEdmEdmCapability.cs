using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdmEdmCapability : EdmCapability
    {
        public RebootEdmEdmCapability(Edmldentifier getldentifier, CommandDefinition[] commandDefinitios, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitios;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }
        public Edmldentifier Getldentifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttribtueDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}