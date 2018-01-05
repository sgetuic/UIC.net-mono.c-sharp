namespace UIC.Framework.Interfaces.Project
{
    public interface DatapointTaskMetadata
    {
        double ExpectedMaximum { get; }
        double ExpectedMinimum { get; }
        double WarningThreshold { get; }
        double ErrorThreshold { get; }
        bool IslnverseThresholdEvaluation { get; }
        string Tags { get; }
    }
}