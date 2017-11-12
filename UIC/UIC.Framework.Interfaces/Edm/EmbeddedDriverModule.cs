using System;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framework.Interfaces.Edm
{
    public interface EmbeddedDriverModule
    {
        Edmldentifier Identifier { get; }

        void Initialize();
        void Dispose();

        EdmCapability GetCapability();

        DatapointValue GetValueFor(DatapointDefinition datapoint);
        AttributeValue GetValueFor(AttribtueDefinition attribtue);

        bool Handle(Command command);

        void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback);
        void SetAttributeCallback(AttribtueDefinition attribtueDefinition, Action<AttributeValue> callback);
    }
}