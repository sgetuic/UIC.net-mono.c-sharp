using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.I2c.Adafruit.VCNL4010
{
    internal class Vlnc4010EdmCapability : EdmCapability
    {
        public Vlnc4010EdmCapability(EdmIdentifier identifier, CommandDefinition[] commandDefinitions, AttributeDefinition[] attributeDefinitions, DatapointDefinition[] datapointDefinitions) {
            Identifier = identifier;
            CommandDefinitions = commandDefinitions;
            AttributeDefinitions = attributeDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }

        public EdmIdentifier Identifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttributeDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}