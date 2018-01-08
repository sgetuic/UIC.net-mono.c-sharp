using UIC.Framework.Interfaces.Project;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetDatapointTaskReportingCondition : DatapointTaskReportingCondition
    {
        public SgetDatapointTaskReportingCondition(double reportingThreshoIdInpercent, double minimalAhsoluteChange, long reportingThresholdInMilliSecs) {
            ReportingThreshoIdInpercent = reportingThreshoIdInpercent;
            MinimalAhsoluteChange = minimalAhsoluteChange;
            ReportingThresholdInMilliSecs = reportingThresholdInMilliSecs;
        }
        public double ReportingThreshoIdInpercent { get; }
        public double MinimalAhsoluteChange { get; }
        public long ReportingThresholdInMilliSecs { get; }
    }
}