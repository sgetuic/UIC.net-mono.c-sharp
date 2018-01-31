using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation

{
    public class SgetAttributDefinition : AttributeDefinition
    {
        public SgetAttributDefinition(Guid id, string uri, string label, UicDataType dataType, string description) {
            Id = id;
            Label = label;
            DataType = dataType;
            Description = description;
            Uri = uri;
        }
        public Guid Id { get; }
        public string Label { get; }
        public string Description { get; }
        public UicDataType DataType { get; }
        public string Uri { get; }

        public override string ToString()
        {
            return Uri;
        }
    }
}