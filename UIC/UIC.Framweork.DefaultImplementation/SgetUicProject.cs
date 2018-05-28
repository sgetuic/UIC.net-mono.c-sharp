using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetUicProject: UicProject
    {
        [JsonConstructor]
        public SgetUicProject(string projectKey, string name, string description, string owner, Guid customerForeignKey, List<SgetAttributDefinition> attributes, List<SgetProjectDatapointTask> datapointTasks) {
            ProjectKey = projectKey;
            Name = name;
            Description = description;
            Owner = owner;
            CustomerForeignKey = customerForeignKey;
            Attributes = attributes.Select(a => (AttributeDefinition)a).ToList();
            DatapointTasks = datapointTasks.Select(a => (ProjectDatapointTask)a).ToList();
        }

        public SgetUicProject(string projectKey, string name, string description, string owner, Guid customerForeignKey, List<AttributeDefinition> attributes, List<ProjectDatapointTask> datapointTasks) {
            ProjectKey = projectKey;
            Name = name;
            Description = description;
            Owner = owner;
            CustomerForeignKey = customerForeignKey;
            Attributes = attributes.Select(a => (AttributeDefinition)a).ToList();
            DatapointTasks = datapointTasks.Select(a => (ProjectDatapointTask)a).ToList();
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