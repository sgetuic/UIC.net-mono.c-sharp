using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framework.Interfaces.Communication
{
    public interface CommunicationAgent
    {
        void Connect(Action<Command> commandHandler);
        void Dispose();
        void Synchronize(string serialId, UicProject project);
        void Push(DatapointValue value);
        void Push(IEnumerable<DatapointValue> values);
        void Push(AttributeValue value);
        void Push(IEnumerable<AttributeValue> values) ;
        void Debug(string debug);
    }
}
