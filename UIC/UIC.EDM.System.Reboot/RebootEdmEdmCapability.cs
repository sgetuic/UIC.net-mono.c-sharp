using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdmEdmCapability : EdmCapability
    {
        public RebootEdmEdmCapability(Edmldentifier getldentifier, IEnumerable<CommandDefinition> commandDefinitios, IEnumerable<AttribtueDefinition> attribtueDefinitions, IEnumerable<DatapointDefinition> datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitios;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }
        public Edmldentifier Getldentifier { get; }
        public IEnumerable<CommandDefinition> CommandDefinitions { get; }
        public IEnumerable<AttribtueDefinition> AttribtueDefinitions { get; }
        public IEnumerable<DatapointDefinition> DatapointDefinitions { get; }
    }
}