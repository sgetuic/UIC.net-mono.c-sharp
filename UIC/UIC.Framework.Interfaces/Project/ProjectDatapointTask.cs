using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.Framework.Interfaces.Project
{
    public interface ProjectDatapointTask
    {
        long PollIntervall { get; }
        string Description { get; }
        DatapointDefinition Definition { get; }
        DatapointTaskReportingCondition ReportingCondition { get; }
        DatapointTaskMetadata MetaData { get; }
    }
}