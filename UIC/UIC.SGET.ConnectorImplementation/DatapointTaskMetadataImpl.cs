using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.SGET.ConnectorImplementation
{
    public class DatapointTaskMetadataImpl : DatapointTaskMetadata
    {
        public string ProjectKey {  get; set; }

        public double ExpectedMaximum { get; set; }

        public double ExpectedMinimum { get; set; }

        public double WarningThreshold  { get; set; }

        public double ErrorThreshold { get; set; }

        public bool IslnverseThresholdEvaluation { get; set; }

        public string Tags { get; set; }
    }
}
