using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.EDM.Test.Mockup
{
    public class MockupEdmEdmCapability : EdmCapability
    {
        public MockupEdmEdmCapability(Edmldentifier getldentifier, CommandDefinition[] commandDefinitios, AttributeDefinition[] attribtueDefinitions, DatapointDefinition[] datapointDefinitions) {
            Getldentifier = getldentifier;
            CommandDefinitions = commandDefinitios;
            AttribtueDefinitions = attribtueDefinitions;
            DatapointDefinitions = datapointDefinitions;
        }
        public Edmldentifier Getldentifier { get; }
        public CommandDefinition[] CommandDefinitions { get; }
        public AttributeDefinition[] AttribtueDefinitions { get; }
        public DatapointDefinition[] DatapointDefinitions { get; }
    }
}