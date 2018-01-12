using System;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Util;
using UIC.SGET.ConnectorImplementation.Monitoring.Evaluation;

namespace UIC.SGET.ConnectorImplementation.Monitoring
{
    internal class DataPointEvaluatorProvider
    {
        public IDataPointEvaluator ProvideFor(DatapointValue val, DataPointEvaluatorParam param)
        {
            switch (val.Definition.DataType)
            {
                case UicDataType.Integer:
                    return new DataPointEvaluatorIntImpl(param);
                case UicDataType.Double:
                    return new DataPointEvaluatorDoubleImpl(param);
                case UicDataType.Bool:
                    return new DataPointEvaluatorBoolImpl(param);
                case UicDataType.String:
                    return new DataPointEvaluatorStringImpl(param);
                default:
                    throw new Exception("Cannot provide Evaluator for val.GetDefinition().DataType");
            }
        }
    }
}