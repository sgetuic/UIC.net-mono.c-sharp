using System.Collections.Generic;

namespace UIC.Framework.Interfaces.Edm.Definition
{
    public interface EdmCapability
    {
        Edmldentifier Getldentifier { get; }
        IEnumerable<CommandDefinition> CommandDefinitios { get; }
        IEnumerable<AttribtueDefinition> AttribtueDefinitions { get; }
        IEnumerable<DatapointDefinition> DatapointDefinitions { get; }
        
    }
}
