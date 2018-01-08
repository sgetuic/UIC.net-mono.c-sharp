using UIC.Framework.Interfaces.Project;

namespace UIC.Framweork.DefaultImplementation
{
    public class SgetDatapointTaskMetadata : DatapointTaskMetadata
    {
        public SgetDatapointTaskMetadata(double expectedMaximum, double expectedMinimum, double warningThreshold, double errorThreshold, bool islnverseThresholdEvaluation, string tags) {
            ExpectedMaximum = expectedMaximum;
            ExpectedMinimum = expectedMinimum;
            WarningThreshold = warningThreshold;
            ErrorThreshold = errorThreshold;
            IslnverseThresholdEvaluation = islnverseThresholdEvaluation;
            Tags = tags;
        }
        public double ExpectedMaximum { get; }
        public double ExpectedMinimum { get; }
        public double WarningThreshold { get; }
        public double ErrorThreshold { get; }
        public bool IslnverseThresholdEvaluation { get; }
        public string Tags { get; }
    }
}