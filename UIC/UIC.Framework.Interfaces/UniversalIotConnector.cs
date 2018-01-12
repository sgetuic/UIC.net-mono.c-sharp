using System;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.Framework.Interfaces
{
    public interface UniversalIotConnector : IDisposable
    {
        string SerialId { get; }
        void Initialize();


        EmbeddedDriverModule GetEdmFor(Guid definitionId);
        void Push(DatapointValue val);
    }
}