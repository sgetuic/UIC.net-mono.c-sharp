using System;

namespace UIC.Framework.Interfaces.Edm
{
    public interface EdmIdentifier
    {
        Guid Id { get; }
        string Uri { get; }
    }
}