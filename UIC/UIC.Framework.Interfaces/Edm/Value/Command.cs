using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Edm.Value
{
    // Command definition kommt von EDM
    // 
    public interface Command
    {
        CommandDefinition CommandDefinition { get; }
        string Payload { get; }
    }
}