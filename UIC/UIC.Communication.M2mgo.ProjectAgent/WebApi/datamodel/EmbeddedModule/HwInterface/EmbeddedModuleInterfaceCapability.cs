using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Command;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface
{
    public class EmbeddedModuleHwInterfaceCapability
    {
        public EmbeddedHwInterfaceIdentifier InterfaceIdentifier { get; private set; }

        public IEnumerable<SGetCloudMapperCommandDefinition> Commands { get; private set; }
        public IEnumerable<SGetCloudMapperAttributeDefinition> Attributes { get; private set; }
        public IEnumerable<SGetCloudMapperDataPointDefinition> DataPoints { get; private set; }

        public EmbeddedModuleHwInterfaceCapability(EmbeddedHwInterfaceIdentifier interfaceIdentifier, 
            IEnumerable<SGetCloudMapperAttributeDefinition> attributes, 
            IEnumerable<SGetCloudMapperDataPointDefinition> dataPoints, 
            IEnumerable<SGetCloudMapperCommandDefinition> commands)
        {
            Commands = commands ?? new SGetCloudMapperCommandDefinition[0];
            DataPoints = dataPoints ?? new SGetCloudMapperDataPointDefinition[0];
            Attributes = attributes ?? new SGetCloudMapperAttributeDefinition[0];
            InterfaceIdentifier = interfaceIdentifier;
        }

        internal IEnumerable<SGetCloudMapperCommandDefinition> GetCommandsFor(SgetProjectDataPointTask datapointTask)
        {
            return Commands.Where(c => c.RelatedSensorKey == null || c.RelatedSensorKey.Key == datapointTask.Info.Key).ToArray();
        }
    }
}