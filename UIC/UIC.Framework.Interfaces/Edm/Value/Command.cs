using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Edm.Value
{
    public class Command
    {
        public CommandDefinition CommandDefinition { get; }
        public object Payload { get; }
    }
}