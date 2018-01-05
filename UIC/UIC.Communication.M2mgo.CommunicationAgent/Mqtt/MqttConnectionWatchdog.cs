using System;
using System.Threading;
using uPLibrary.Networking.M2Mqtt;
using UIC.Util.Logging;

namespace UIC.Communication.M2mgo.CommunicationAgent.Mqtt
{
    internal class MqttConnectionWatchdog
    {
        private MqttClient _mqttClient;
        private readonly ILogger _logger;
        private bool _isDispoed;
        private readonly object _connectionMutex = new object();

        public MqttConnectionWatchdog(ILogger logger)
        {
            _logger = logger;
        }

        private void MqttClientOnConnectionClosed(object sender, EventArgs e)
        {
            _logger.Information("MqttClientOnConnectionClosed: " + e);
            Conntect();
        }

        internal void Connect(MqttClient mqttClient)
        {
            _mqttClient = mqttClient;
            _mqttClient.ConnectionClosed += MqttClientOnConnectionClosed;
            Conntect();
        }

        private void Conntect() {
            bool retry;
            int retryCnt = 0;
            do {
                if (_isDispoed)
                    return;
                try {
                    lock (_connectionMutex) {
                        if (_mqttClient.IsConnected)
                            return;
                        _mqttClient.Connect(Guid.NewGuid().ToString().Substring(0, 19));
                    }
                    retry = false;
                } catch (Exception e) {
                    _logger.Warning(e.Message);
                    retry = true;
                    retryCnt++;
                    Thread.Sleep(retryCnt * 10 * 1000);
                }
            } while (retry);

            _logger.Information("Conected");
        }

        public void Dispose()
        {
            if (_isDispoed || _mqttClient == null)
                return;

            lock (_connectionMutex)
            {
                if (_isDispoed || _mqttClient == null)
                    return;
                
                _isDispoed = true;
                
                _mqttClient.Disconnect();
            }
        }
    }
}