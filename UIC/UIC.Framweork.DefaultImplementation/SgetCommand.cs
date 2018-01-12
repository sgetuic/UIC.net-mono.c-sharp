using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetCommand : Command
    {
        public SgetCommand(CommandDefinition commandDefinition, string payload) {
            CommandDefinition = commandDefinition;
            Payload = payload;
        }
        public CommandDefinition CommandDefinition { get; }
        public string Payload { get; }
    }
}