using System.Collections.Generic;

namespace UIC.Framework.Interfaces.Edm.Definition
{
    public interface EdmCapability
    {
        Edmldentifier Getldentifier { get; }
        CommandDefinition[] CommandDefinitions { get; }
        AttributeDefinition[] AttributeDefinitions { get; }
        DatapointDefinition[] DatapointDefinitions { get; }
        
    }
}
