using System;
using System.Collections.Generic;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Command;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel {
    public class SgetEmbeddedDriverModuleAppliance {
        public CloudMapperIdentifier CloudMapperIdentifier { get; private set; }
        public IEnumerable<EmbeddedDriverModulCapabilitiy> EmbeddedDriverModules { get; private set; }

        public SgetEmbeddedDriverModuleAppliance(CloudMapperIdentifier cloudMapperIdentifier, IEnumerable<EmbeddedDriverModulCapabilitiy> embeddedDriverModules)
        {
            if (cloudMapperIdentifier == null) throw new ArgumentNullException("cloudMapperIdentifier");
            if (embeddedDriverModules == null) throw new ArgumentNullException("embeddedDriverModules");
            CloudMapperIdentifier = cloudMapperIdentifier;
            EmbeddedDriverModules = embeddedDriverModules;
        }

        public SGetCloudMapperAttributeDefinition GetDefinitionFor(SgetProjectProperty prop)
        {
            foreach (var driverModule in EmbeddedDriverModules)
            {
                SGetCloudMapperAttributeDefinition def = driverModule.GetDefinitionFor(prop);
                if (def != null)
                {
                    return def;
                }
            }
            return null;
        }

        public SGetCloudMapperDataPointDefinition GetDefinitionFor(SgetProjectDataPointTask dataPoint)
        {
            foreach (var driverModule in EmbeddedDriverModules) {
                SGetCloudMapperDataPointDefinition def = driverModule.GetDefinitionFor(dataPoint);
                if (def != null) {
                    return def;
                }
            }
            throw new Exception("Could not get defintion for datapoint task" + dataPoint);
        }
        
        public IEnumerable<SGetCloudMapperCommandDefinition> GetCommandsFor(SgetProjectDataPointTask datapointTask)
        {
            foreach (var driverModule in EmbeddedDriverModules)
            {
                IEnumerable<SGetCloudMapperCommandDefinition> commandList = driverModule.GetCommandsFor(datapointTask);
                if (commandList != null)
                {
                    return commandList;
                }
            }
            return new SGetCloudMapperCommandDefinition[0];
        }
    }
}
