using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmCapability : EdmCapability
    {
        public EapiBoardInformationEdmCapability(EdmIdentifier getldentifier, CommandDefinition[] commandDefinitions, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Identifier = getldentifier;
            CommandDefinitions = commandDefinitions;
            AttributeDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }

        public EdmIdentifier Identifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttributeDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}