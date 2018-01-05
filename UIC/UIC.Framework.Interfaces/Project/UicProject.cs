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

        IEnumerable<AttributeDefinition> Attributes { get; }
        IEnumerable<ProjectDatapointTask> DatapointTasks { get; }
    }
}
