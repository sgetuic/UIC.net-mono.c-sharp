using System;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framework.Interfaces.Edm.Definition
{
    public interface AttributeDefinition
    {
        Guid Id { get; }
        string Label { get; }
        string Description { get; }
        UicDataType DataType { get; }
        string Uri { get; }
    }
}