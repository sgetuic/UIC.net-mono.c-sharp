using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.SGET.ConnectorImplementation
{
    public class ProjectDatapointTaskImpl : ProjectDatapointTask
    {

        public ProjectDatapointTaskImpl(DatapointDefinitionImpl Definition, DatapointTaskReportingConditionImpl ReportingCondition, DatapointTaskMetadataImpl MetaData)
        {
            this.Definition = Definition;
            this.ReportingCondition = ReportingCondition;
            this.MetaData = MetaData;
        }




        public string ProjectKey {  get; set; }

        public long PollIntervall { get; set; }

        public string Description { get; set; }

        public DatapointDefinition Definition { get; set; }

        public DatapointTaskReportingCondition ReportingCondition { get; set; }

        public DatapointTaskMetadata MetaData { get; set; }
    }
}
