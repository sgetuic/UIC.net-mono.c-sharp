using System;
using System.Threading;
using UIC.Framework.Interfaces;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.SGET.ConnectorImplementation.Monitoring.Evaluation;
using UIC.Util.Logging;

namespace UIC.SGET.ConnectorImplementation.Monitoring {
    internal class DatapointMonitor : IDisposable {
       private readonly DataPointEvaluatorProvider _evaluatorProvider;
        private readonly UniversalIotConnector _connector;
        private readonly ILogger _logger;
        private bool _isDisposed;
        private IDataPointEvaluator _evaluator;
        private readonly ProjectDatapointTask _dataPointTask;

        public DatapointMonitor(ProjectDatapointTask dataPointTask, DataPointEvaluatorProvider evaluatorProvider, UniversalIotConnector connector, ILogger logger)
        {
            if (dataPointTask == null) throw new ArgumentNullException("dataPointTask");
            if (logger == null) throw new ArgumentNullException("logger");
            
            _dataPointTask = dataPointTask;
            _evaluatorProvider = evaluatorProvider;
            _connector = connector ?? throw new ArgumentNullException(nameof(connector));
            _logger = logger;
            (new Thread(Target)).Start();
        }

        
        private void Target()
        {
            try {
                long pollIntervall = _dataPointTask.PollIntervall;

                if (pollIntervall <= 0)
                {
                    _logger.Warning("Set poll intervall is to short: " + pollIntervall);
                    pollIntervall = 10;
                }
                _logger.Information("Start Monitoring with intervall in seconds: " + pollIntervall);

                var edm = _connector.GetEdmFor(_dataPointTask.Definition.Id);

                while (!_isDisposed) {
                    if (_dataPointTask.Definition == null) {
                        _logger.Warning("Skip Datapoint evaluation because the EDM might not be installed correctly");
                    } else {
                        try
                        {
                            _logger.Information("ReadDatapoint: " + _dataPointTask.Definition);
                            DatapointValue val = edm.GetValueFor(_dataPointTask.Definition);
                            if (Evaluate(val))
                            {
                                _connector.Push(val);
                            }
                        }
                        catch (Exception e)
                        {
                            _logger.Warning(e.Message);
                        }
                    }
                    for (int i = 0; i < pollIntervall; i++) {
                        if (_isDisposed)
                            break;
                        Thread.Sleep(1000);
                    }
                    
                }
            } catch (Exception e) {
                _logger.Error(e);
            }
        }

        private bool Evaluate(DatapointValue val)
        {
            if (_evaluator == null)
            {
                _evaluator = _evaluatorProvider.ProvideFor(val, new DataPointEvaluatorParam(_dataPointTask.ReportingCondition));
            }
            return _evaluator.ShouldSendToCloud(val);
        }

      

        public void Dispose()
        {
            _isDisposed = true;

        }
    }
}
