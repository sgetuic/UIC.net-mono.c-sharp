using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetProjectDatapointTask : ProjectDatapointTask
    {
        public SgetProjectDatapointTask(DatapointDefinition definition, DatapointTaskReportingCondition reportingCondition, long pollIntervall, DatapointTaskMetadata metaData, string description) {
            Definition = definition;
            ReportingCondition = reportingCondition;
            MetaData = metaData;
            PollIntervall = pollIntervall;
            Description = description;
        }
        public long PollIntervall { get; }
        public string Description { get; }
        public DatapointDefinition Definition { get; }
        public DatapointTaskReportingCondition ReportingCondition { get; }
        public DatapointTaskMetadata MetaData { get; }
    }
}