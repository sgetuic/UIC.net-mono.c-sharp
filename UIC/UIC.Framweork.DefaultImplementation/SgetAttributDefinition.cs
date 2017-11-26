using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framweork.DefaultImplementation

{
    public class SgetAttributDefinition : AttribtueDefinition
    {
        public SgetAttributDefinition(Guid id, string label, UicDataType dataType, string description) {
            Id = id;
            Label = label;
            DataType = dataType;
            Description = description;
        }
        public Guid Id { get; }
        public string Label { get; }
        public string Description { get; }
        public UicDataType DataType { get; }
    }
}