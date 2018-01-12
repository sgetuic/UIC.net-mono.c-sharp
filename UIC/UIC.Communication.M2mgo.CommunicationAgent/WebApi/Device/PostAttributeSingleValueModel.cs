using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Device {
    class PostAttributeSingleValueModel {
        public Guid DeviceType { get; set; }
        public string DeviceName { get; set; }
        public string AttributeName { get; set; }
        public string Value { get; set; }
    }
}
