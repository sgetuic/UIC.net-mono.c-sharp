using System;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorLongImpl : DataPointEvaluator<long> {
        public DataPointEvaluatorLongImpl(DataPointEvaluatorParam param)
            : base(param) {
            }

        protected override bool EvalVariance(long val) {
            if (!AnyAlreadyReportedValues())
                return true;
            double difference = Math.Abs(LastTriggeredDataPoint - val);
            double variance = (Math.Abs(LastTriggeredDataPoint - val) * 100 / (double)(LastTriggeredDataPoint == 0 ? 1 : LastTriggeredDataPoint));

            return EvalVariance(variance, difference);
        }

        protected override long GetTypedValue(DatapointValue val)
        {
            return (long) val.Value;
        }
    }
}