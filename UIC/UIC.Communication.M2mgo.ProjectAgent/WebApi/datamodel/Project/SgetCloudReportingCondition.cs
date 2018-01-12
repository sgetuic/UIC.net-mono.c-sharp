namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project
{
    public class SgetCloudReportingCondition
    {
        public long ReportingThresholdInMilliSecs { get; private set; }
        public double ReportingThresholdInPercent { get; private set; }
        public double MinimalAbsoluteDifference { get; private set; }
        public bool NoMinimalDiffSet { get; private set; }
        
        public SgetCloudReportingCondition(long? reportingThresholdInMilliSecs, double? reportingThresholdInPercent, double? minimalAbsoluteDifference) {
            ReportingThresholdInMilliSecs = reportingThresholdInMilliSecs ?? long.MaxValue;
            if ((long.MaxValue / 1000) > reportingThresholdInMilliSecs)
            {
                ReportingThresholdInMilliSecs *= 1000;
            }
            ReportingThresholdInPercent = reportingThresholdInPercent ?? double.MaxValue;
            MinimalAbsoluteDifference = minimalAbsoluteDifference ?? -1;
            NoMinimalDiffSet = MinimalAbsoluteDifference <= 0;
        }
    }
}