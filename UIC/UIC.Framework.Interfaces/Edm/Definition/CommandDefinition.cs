using System;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framework.Interfaces.Edm.Definition
{
    public interface CommandDefinition
    {
        Guid Id { get; }
        string Uri { get; }
        string Label { get; }
        string Description { get; }
        string Command { get; }
        UicDataType DataType { get; }
        string[] Tags { get; }
        DatapointDefinition RelatedDatapoint { get; }
    }
}