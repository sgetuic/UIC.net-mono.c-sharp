using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Project
{
    public interface UicProject
    {
        string ProjectKey { get; }
        string Name { get; }
        string Description { get; }
        string Owner { get; }
        Guid CustomerForeignKey { get; }

        List<AttributeDefinition> Attributes { get; }
        List<ProjectDatapointTask> DatapointTasks { get; }
    }
}
