using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmCapability : EdmCapability
    {
        public EapiBoardInformationEdmCapability(Edmldentifier getldentifier, CommandDefinition[] commandDefinitions, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitions;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }

        public Edmldentifier Getldentifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttribtueDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}