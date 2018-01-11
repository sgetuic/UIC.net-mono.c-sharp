using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Edm.Value
{
    public interface Command
    {
        CommandDefinition CommandDefinition { get; }
        string Payload { get; }
    }
}