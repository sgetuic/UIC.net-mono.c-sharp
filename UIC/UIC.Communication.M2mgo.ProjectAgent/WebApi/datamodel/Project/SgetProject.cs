using System;
using System.Collections.Generic;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project {
    public class SgetProject {
        public string ProjectKey { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string Owner { get; private set; }
        public Guid CustomerForeignKey { get; private set; }

        public IEnumerable<SgetProjectProperty> Properties { get; private set; }
        public IEnumerable<SgetProjectDataPointTask> DataPointTasks { get; private set; }

        public IEnumerable<SgetProjectDataPointTask> ExternalDataPointTasks { get; private set; }

        public SgetProject(string projectKey, string name, string description, string owner, string customerForeignKey, IEnumerable<SgetProjectProperty> properties, IEnumerable<SgetProjectDataPointTask> dataPointTasks) {
            ProjectKey = projectKey;
            Name = name;
            Description = description;
            Owner = owner;
            CustomerForeignKey = new Guid(customerForeignKey);
            Properties = properties ?? new SgetProjectProperty[0];
            DataPointTasks = dataPointTasks ?? new SgetProjectDataPointTask[0];
            ExternalDataPointTasks = new SgetProjectDataPointTask[0];
        }

        public void AddExternalDatapoints(IEnumerable<SgetProjectDataPointTask> externalDatapointTasks)
        {
            ExternalDataPointTasks = externalDatapointTasks;
        }   
    }
}
