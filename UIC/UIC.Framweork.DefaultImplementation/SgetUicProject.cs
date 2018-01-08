using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetUicProject: UicProject
    {
        public SgetUicProject(string projectKey, string name, string description, string owner, Guid customerForeignKey, List<AttributeDefinition> attributes, List<ProjectDatapointTask> datapointTasks) {
            ProjectKey = projectKey;
            Name = name;
            Description = description;
            Owner = owner;
            CustomerForeignKey = customerForeignKey;
            Attributes = attributes;
            DatapointTasks = datapointTasks;
        }
        public string ProjectKey { get; private set; }
        public string Name { get; private set;}
        public string Description { get; private set;}
        public string Owner { get; private set;}
        public Guid CustomerForeignKey { get; private set; }
        public List<AttributeDefinition> Attributes { get; private set;}
        public List<ProjectDatapointTask> DatapointTasks { get; private set;}
    }
}