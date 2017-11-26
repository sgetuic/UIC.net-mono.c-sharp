using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetAttributeValue : AttributeValue
    {
        public SgetAttributeValue(object value, AttribtueDefinition definition) {
            Value = value;
            Definition = definition;
        }
        public AttribtueDefinition Definition { get; }
        public object Value { get; }
    }
}