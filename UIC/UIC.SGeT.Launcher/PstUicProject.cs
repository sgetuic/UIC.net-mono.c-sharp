using System;
using System.Collections.Generic;
using UIC.Util.Logging;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.EDM.EApi.BoardInformation;

namespace UIC.EDM.Test.Mockup
{
    public class PstUicProject : SgetUicProject
    {
        public PstUicProject(ILoggerFactory loggerFactory) : base(
            "26895846c960465ebd89f28d10e6460c",
            "JU Test",
            "Main JU Test Project",
            "SGeT",
            Guid.NewGuid(),
            new List<SgetAttributDefinition>(),
            new List<SgetProjectDatapointTask>())
        {
            EapiBoardInformationEdm eapiBoardInformation = new EapiBoardInformationEdm();
            foreach (var attr in eapiBoardInformation.GetCapability().AttributeDefinitions)
            {
                Attributes.Add(attr);
            }

            // TODO: commands from EapiGPIO

            MockupEdm mockupEdm = new MockupEdm(loggerFactory);
            DatapointTasks.Add(new SgetProjectDatapointTask(
                new SgetDatapointDefinition(new Guid("{83f02bea-c22b-46aa-b1c2-4ab8102d8a80}"), UicUriBuilder.DatapointFrom(mockupEdm, "Bool_mock"), UicDataType.Bool, "Random Bool", "Digital input mockup"),
                new SgetDatapointTaskReportingCondition(50, -1, 50000),
                10,
                new SgetDatapointTaskMetadata(0, 0, 0, 0, false, "SwitchButton"),
                "Random Bool"));

            DatapointTasks.Add(new SgetProjectDatapointTask(
                new SgetDatapointDefinition(new Guid("{4087d40d-d4e2-42b1-89a4-9b9d18499a04}"), UicUriBuilder.DatapointFrom(mockupEdm, "Integer_mock"), UicDataType.Integer, "Random Integer", "Integer measurement mockup"),
                new SgetDatapointTaskReportingCondition(50, 0, 30000),
                5,
                new SgetDatapointTaskMetadata(0, 0, 0, 0, false, "SimpleValue"),
                "Random Integer"));
        }
    }
}
