using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Edm.Definition;

namespace UIC.SGET.ConnectorImplementation
{
    public class DatapointTaskReportingConditionImpl : DatapointTaskReportingCondition
    {

        public double ReportingThreshoIdInpercent { get; set; }

        public double MinimalAhsoluteChange { get; set; }

        public long ReportingThresholdInMilliSecs { get; set; }
}
}
