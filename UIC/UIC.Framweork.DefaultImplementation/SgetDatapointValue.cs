using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation

{
    public class SgetDatapointValue : DatapointValue
    {
        public SgetDatapointValue(object value, DatapointDefinition definition) {
            Value = value;
            Definition = definition;
        }
        public DatapointDefinition Definition { get; }
        public object Value { get; }
    }
}