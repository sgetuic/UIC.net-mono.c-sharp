using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.SGET.ConnectorImplementation
{
    public class ProjectConfigImpl : UicProject
    {


        public ProjectConfigImpl(string ProjectKey, string name, string Description, string owner, Guid CustomerForeignKey, List<AttributeDefinitionImpl> Attributes, List<ProjectDatapointTaskImpl> DatapointTasks)
        {

            this.Attributes = new List<AttributeDefinition>();
            foreach(AttributeDefinitionImpl impl in Attributes){
                this.Attributes.Add(impl);
            }


            this.DatapointTasks = new List<ProjectDatapointTask>();
            foreach (ProjectDatapointTaskImpl impl in DatapointTasks)
            {
                this.DatapointTasks.Add(impl);
            }



            this.Name = Name;
            this.ProjectKey = ProjectKey;
            this.Description = Description;
            this.Owner = Owner;
            this.CustomerForeignKey = CustomerForeignKey;


        }

        public string ProjectKey {  get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string Owner { get; set; }

        public Guid CustomerForeignKey { get; set; }

        public List<AttributeDefinition> Attributes { get; set; }

        public List<ProjectDatapointTask> DatapointTasks { get; set; }
    }
}
