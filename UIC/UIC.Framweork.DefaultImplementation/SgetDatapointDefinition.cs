using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetDatapointDefinition : DatapointDefinition
    {
        public SgetDatapointDefinition(Guid id, string uri, UicDataType dataType, string label, string description) {
            Id = id;
            Label = label;
            Description = description;
            Uri = uri;
            DataType = dataType;
        }
        public Guid Id { get; }
        public string Label { get; }
        public string Description { get; }
        public UicDataType DataType { get; }
        public string Uri { get;  }

        public override string ToString()
        {
            return Uri;
        }
    }

}