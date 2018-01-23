using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.Gpio
{
    internal class GpioEdmCapability : EdmCapability
    {
        public GpioEdmCapability(EdmIdentifier identifier, DatapointDefinition[] datapointDefinitions, AttributeDefinition[] attributeDefinitions, CommandDefinition[] commandDefinitions) {
            Identifier = identifier;
            DatapointDefinitions = datapointDefinitions;
            AttributeDefinitions = attributeDefinitions;
            CommandDefinitions = commandDefinitions;
        }

        public EdmIdentifier Identifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttributeDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}