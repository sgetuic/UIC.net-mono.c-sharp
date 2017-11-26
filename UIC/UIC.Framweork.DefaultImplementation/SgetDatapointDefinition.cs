using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetDatapointDefinition : DatapointDefinition
    {
        public SgetDatapointDefinition(Guid id, UicDataType dataType, string key, string label, string description) {
            Id = id;
            Key = key;
            Label = label;
            Description = description;
            DataType = dataType;
        }
        public Guid Id { get; }
        public string Key { get; }
        public string Label { get; }
        public string Description { get; }
        public UicDataType DataType { get; }
    }
}