using System;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto
{
    public class Sensor {
        private string _sensorKey;
        public Identifier Identifier { get; set; }

        public string SensorKey
        {
            get { return _sensorKey; }
            set
            {
                if(value.Length > 50) throw new ArgumentException("SensorKey mus be less than 50 characters: " + value);
                _sensorKey = value;
            }
        }

        public string Name { get; set; }
        public string Label { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Units { get; set; }
        public int DataType { get; set; }
        public Identifier DeviceTypeIdentifier { get; set; }

        public SensorMetadataViewModel Metadata { get; set; }

    }
}