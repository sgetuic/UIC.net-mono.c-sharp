using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public interface IDataPointEvaluator
    {
        bool ShouldSendToCloud(DatapointValue val);
    }
}