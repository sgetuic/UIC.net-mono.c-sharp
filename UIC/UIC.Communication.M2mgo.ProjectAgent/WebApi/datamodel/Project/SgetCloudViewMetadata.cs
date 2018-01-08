namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project
{
    public class SgetCloudViewMetadata
    {
        public double? ErrorThreshold { get; private set; }
        public string[] Tags { get; private set; }
        public bool IsInverseThresholdEvaluation { get; private set; }
        public double? Max { get; private set; }
        public double? Min { get; private set; }
        public double? WarningThreshold { get; private set; }

        public SgetCloudViewMetadata(double? errorThreshold, string[] tags, bool isInverseThresholdEvaluation, double? max, double? min, double? warningThreshold) {
            WarningThreshold = warningThreshold;
            Min = min;
            Max = max;
            IsInverseThresholdEvaluation = isInverseThresholdEvaluation;
            Tags = tags;
            ErrorThreshold = errorThreshold;
        }
    }
}