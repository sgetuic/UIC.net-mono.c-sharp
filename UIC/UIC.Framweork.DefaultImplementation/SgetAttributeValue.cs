using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetAttributeValue : AttributeValue
    {
        public SgetAttributeValue(object value, AttributeDefinition definition) {
            Value = value;
            Definition = definition;
        }
        public AttributeDefinition Definition { get; }
        public object Value { get; }

        public override string ToString()
        {
            return Value + " - " + Definition;
        }
    }
}