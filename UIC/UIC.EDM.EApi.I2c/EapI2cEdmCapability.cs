using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.EApi.I2c
{
    public class EapI2cEdmCapability : EdmCapability
    {
        public EapI2cEdmCapability(Edmldentifier getldentifier, CommandDefinition[] commandDefinitions, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitions;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }

        public Edmldentifier Getldentifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttribtueDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}