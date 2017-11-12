using System;

namespace UIC.Framework.Interfaces.Edm
{
    public interface Edmldentifier
    {
        Guid Id { get; }
        string Name { get; }
    }
}