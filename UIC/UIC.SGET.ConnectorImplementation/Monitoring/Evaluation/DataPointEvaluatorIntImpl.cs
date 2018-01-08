using System;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorIntImpl : DataPointEvaluator<int> {
        public DataPointEvaluatorIntImpl(DataPointEvaluatorParam param)
            : base(param) {
            }

        protected override bool EvalVariance(int val) {
            if (!AnyAlreadyReportedValues())
                return true;
            double difference = Math.Abs(LastTriggeredDataPoint - val);
            double variance = (difference * 100 / (LastTriggeredDataPoint == 0 ? 1 : LastTriggeredDataPoint));

            return EvalVariance(variance, difference);
        }

        protected override int GetTypedValue(DatapointValue val)
        {
            return (int)val.Value;
        }
    }
}