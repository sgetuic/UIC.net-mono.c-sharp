using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmCapability : EdmCapability
    {
        public EapiBoardInformationEdmCapability(Edmldentifier getldentifier, IEnumerable<CommandDefinition> commandDefinitions, IEnumerable<AttributeDefinition> attribtueDefinitions, IEnumerable<DatapointDefinition> datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitions;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }

        public Edmldentifier Getldentifier { get; }
        public IEnumerable<CommandDefinition> CommandDefinitions { get; }
        public IEnumerable<AttributeDefinition> AttribtueDefinitions { get; }
        public IEnumerable<DatapointDefinition> DatapointDefinitions { get; }
    }
}