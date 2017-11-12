namespace UIC.Framework.Interfaces.Project
{
    public interface DatapointTaskReportingCondition
    {
        double ReportingThreshoIdInpercent { get; }
        double MinimalAhsoluteChange { get; }
        double MinimalPersentageChange { get; }
    }
}