using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Edm.Value
{
    public interface DatapointValue
    {
        DatapointDefinition Definition { get; }
        object Value { get; }
    }
}