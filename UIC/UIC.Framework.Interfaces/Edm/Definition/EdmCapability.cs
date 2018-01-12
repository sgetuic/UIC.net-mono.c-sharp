using System.Collections.Generic;

namespace UIC.Framework.Interfaces.Edm.Definition
{
    public interface EdmCapability
    {
        Edmldentifier Getldentifier { get; }
        CommandDefinition[] CommandDefinitions { get; }
        AttributeDefinition[] AttribtueDefinitions { get; }
        DatapointDefinition[] DatapointDefinitions { get; }
        
    }
}
