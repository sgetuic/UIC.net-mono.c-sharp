namespace UIC.Framework.Interfaces.Project
{
    public interface DatapointTaskReportingCondition
    {
        double ReportingThreshoIdInpercent { get; }
        double MinimalAhsoluteChange { get; }
        long ReportingThresholdInMilliSecs { get;  }
    }
}