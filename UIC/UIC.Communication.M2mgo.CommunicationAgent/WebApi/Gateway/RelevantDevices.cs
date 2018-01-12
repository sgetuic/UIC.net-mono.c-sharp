using System;
using System.Collections.Generic;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Gateway {
    public class RelevantDevice {
        public string DeviceTypeName { get; set; }
        public Guid DeviceTypeDomain { get; set; }
        public string DeviceCustomID { get; set; }
        public IEnumerable<RelevantSensor> RelevantSensors { get; set; }
    }
}
