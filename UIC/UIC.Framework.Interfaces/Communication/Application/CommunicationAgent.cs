using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framework.Interfaces.Communication.Application
{
    public interface CommunicationAgent
    {
        void Connect(Action<Command> commandHandler);
        void Dispose();
        void Initialize(string serialId, UicProject project, List<EmbeddedDriverModule> edms);
        void Push(DatapointValue value);
        void Push(IEnumerable<DatapointValue> values);
        void Push(AttributeValue value);
        void Push(IEnumerable<AttributeValue> values) ;
        void Debug(string debug);
    }
}
