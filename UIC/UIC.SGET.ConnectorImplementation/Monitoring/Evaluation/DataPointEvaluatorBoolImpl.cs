using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorBoolImpl : DataPointEvaluator<bool> {
        public DataPointEvaluatorBoolImpl(DataPointEvaluatorParam param)
            : base(param) {
            }

        protected override bool EvalVariance(bool val) {
            if (!AnyAlreadyReportedValues())
                return true;
            double difference = LastTriggeredDataPoint == val ? 0 : double.MaxValue;
            double variance = LastTriggeredDataPoint == val ? 0 : double.MaxValue;

            return EvalVariance(variance, difference);
        }

        protected override bool GetTypedValue(DatapointValue val)
        {
            return (bool) val.Value;
        }
    }
}