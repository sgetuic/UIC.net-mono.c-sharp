using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public class DataPointEvaluatorStringImpl : DataPointEvaluator<string> {
        public DataPointEvaluatorStringImpl(DataPointEvaluatorParam param)
            : base(param) {
            }

        protected override bool EvalVariance(string val)
        {
            return true;
        }

        protected override string GetTypedValue(DatapointValue val)
        {
            return val.ToString();
        }
    }
}