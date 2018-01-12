using System;
using UIC.Framework.Interfaces.Project;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorParam
    {
        public long ReportingThresholdInMilliSecs { get; private set; }
        public double ReportingThresholdInPercent { get; private set; }
        public double MinimalAbsoluteDifference { get; private set; }
        public bool NoMinimalDiffSet { get; private set; }

        public DataPointEvaluatorParam(
            long reportingThresholdInMilliSecs = long.MaxValue, 
            double reportingThresholdInPercent = double.MaxValue, 
            double minimalAbsoluteDifference = -1)
        {
            if (reportingThresholdInMilliSecs < 1000) throw new ArgumentException("reportingThresholdInMilliSecs must not be less 1000");

            ReportingThresholdInMilliSecs = reportingThresholdInMilliSecs;
            ReportingThresholdInPercent = reportingThresholdInPercent;
            MinimalAbsoluteDifference = minimalAbsoluteDifference;
            NoMinimalDiffSet = MinimalAbsoluteDifference <= 0;
        }

        public DataPointEvaluatorParam(DatapointTaskReportingCondition condition)
            : this(condition.ReportingThresholdInMilliSecs, condition.ReportingThreshoIdInpercent, condition.MinimalAhsoluteChange)
        {}
    }
}