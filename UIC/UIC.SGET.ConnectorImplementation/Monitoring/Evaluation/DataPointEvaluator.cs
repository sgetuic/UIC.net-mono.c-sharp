using System;
using UIC.Framework.Interfaces.Edm.Value;

namespace UIC.SGET.ConnectorImplementation.Monitoring.Evaluation
{
    public abstract class DataPointEvaluator<T> : IDataPointEvaluator
    {
        private readonly DataPointEvaluatorParam _param;
        private DateTime? _lastTriggeredTimestamp;
        protected T LastTriggeredDataPoint;


        protected DataPointEvaluator(DataPointEvaluatorParam param)
        {
            _param = param;
        }

        private bool EvalNewValue(T datapoint)
        {
            bool timeThresholdHit = EvalTimeThreshold();
            bool valueVarianceThresholdHit = EvalVariance(datapoint);

            if (timeThresholdHit || valueVarianceThresholdHit)
            {
                LastTriggeredDataPoint = datapoint;
                _lastTriggeredTimestamp = DateTime.UtcNow;
                return true;
            }
            return false;
        }


        protected abstract bool EvalVariance(T val);

        protected bool EvalVariance(double variance, double difference)
        {
            if ((variance > _param.ReportingThresholdInPercent) && (difference > _param.MinimalAbsoluteDifference || _param.NoMinimalDiffSet))
            {
                return true;
            }
            return false;
        }

        protected bool AnyAlreadyReportedValues()
        {
            return _lastTriggeredTimestamp != null;
        }

        private bool EvalTimeThreshold()
        {
            if (!AnyAlreadyReportedValues())
            {
                return true;
            }
            var millis = (DateTime.UtcNow - (_lastTriggeredTimestamp ?? DateTime.MinValue)).TotalMilliseconds;
            if (millis > _param.ReportingThresholdInMilliSecs)
            {
                return true;
            }
            return false;
        }

        public bool ShouldSendToCloud(DatapointValue val)
        {
            T datapoint = GetTypedValue(val);
            return EvalNewValue(datapoint);
        }

        protected abstract T GetTypedValue(DatapointValue val);
    }
}
