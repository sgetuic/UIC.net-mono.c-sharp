using System;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorDoubleImpl : DataPointEvaluator<double>
    {
        public DataPointEvaluatorDoubleImpl(DataPointEvaluatorParam param) : base(param)
        {
            
        }

        protected override bool EvalVariance(double val)
        {
            if (!AnyAlreadyReportedValues())
                return true;
            double difference = Math.Abs(LastTriggeredDataPoint - val);
            double variance = (Math.Abs(LastTriggeredDataPoint - val) * 100 / ((int)LastTriggeredDataPoint == 0 ? 1 : LastTriggeredDataPoint));

            return EvalVariance(variance, difference);
        }

        protected override double GetTypedValue(DatapointValue val)
        {
            return (double)val.Value;
        }
    }
}