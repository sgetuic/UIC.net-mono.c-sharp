namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi.Blueprint.Dto
{
    public class SensorMetadataViewModel
    {
        public string[] Tags { get; set; }
        public double? Max { get; set; }
        public double? Min { get; set; }

        public double? ErrorThreshold { get; set; }
        public double? WarningThreshold { get; set; }

        public bool IsInverseThresholdEvaluation { get; set; } 
    }
}