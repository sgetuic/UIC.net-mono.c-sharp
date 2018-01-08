using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Command;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule
{
    public class EmbeddedDriverModulCapabilitiy
    {
        private readonly Dictionary<string, SGetCloudMapperDataPointDefinition> _keyExternalDatapointDefinitionMap;
        public EmbeddedModuleIdentifier Identifier { get; private set; }
        public IEnumerable<EmbeddedModuleHwInterfaceCapability> Interfaces { get; private set; }

        private readonly Dictionary<Guid, Dictionary<string, SGetCloudMapperAttributeDefinition>> _attibuteCapabilityMap = new Dictionary<Guid, Dictionary<string, SGetCloudMapperAttributeDefinition>>();
        private readonly Dictionary<Guid, Dictionary<string, SGetCloudMapperDataPointDefinition>> _dataPointCapabilityMap = new Dictionary<Guid, Dictionary<string, SGetCloudMapperDataPointDefinition>>();
        

        public EmbeddedDriverModulCapabilitiy(EmbeddedModuleIdentifier embeddedModuleIdentifier, IEnumerable<EmbeddedModuleHwInterfaceCapability> interfaces, Dictionary<string, SGetCloudMapperDataPointDefinition> keyExternalDatapointDefinitionMap)
        {
            if (interfaces == null) throw new ArgumentNullException("interfaces");
            if (keyExternalDatapointDefinitionMap == null)
                throw new ArgumentNullException("keyExternalDatapointDefinitionMap");

            _keyExternalDatapointDefinitionMap = keyExternalDatapointDefinitionMap;

            Identifier = embeddedModuleIdentifier;
            Interfaces = interfaces;

            foreach (EmbeddedModuleHwInterfaceCapability @interface in Interfaces)
            {
                var interfaceAttributeMap = @interface.Attributes.ToDictionary(a => a.Key, a => a);
                _attibuteCapabilityMap.Add(@interface.InterfaceIdentifier.Id, interfaceAttributeMap);

                var interfaceDatapointMap = @interface.DataPoints.ToDictionary(a => a.Key, a => a);
                _dataPointCapabilityMap.Add(@interface.InterfaceIdentifier.Id, interfaceDatapointMap);
            }
        }

        public SGetCloudMapperAttributeDefinition GetDefinitionFor(SgetProjectProperty prop)
        {
            Dictionary<string, SGetCloudMapperAttributeDefinition> hwInterface;
            if (_attibuteCapabilityMap.TryGetValue(prop.Info.HwInterfaceSchemeID, out hwInterface))
            {
                SGetCloudMapperAttributeDefinition definiton;
                if (hwInterface.TryGetValue(prop.Info.Key, out definiton))
                {
                    return definiton;
                }
            }
            return null;
        }

        

        public SGetCloudMapperDataPointDefinition GetDefinitionFor(SgetProjectDataPointTask dataPoint)
        {
            Dictionary<string, SGetCloudMapperDataPointDefinition> hwInterface;
            if (_dataPointCapabilityMap.TryGetValue(dataPoint.Info.HwInterfaceSchemeID, out hwInterface))
            {
                SGetCloudMapperDataPointDefinition definiton;
                if (hwInterface.TryGetValue(dataPoint.Info.Key, out definiton))
                {
                    return definiton;
                }
                if (_keyExternalDatapointDefinitionMap.TryGetValue(dataPoint.Info.Key, out definiton))
                {
                    return definiton;
                }
            }
            return null;
        }

        internal IEnumerable<SGetCloudMapperCommandDefinition> GetCommandsFor(SgetProjectDataPointTask datapointTask)
        {
            var hwInterface = Interfaces.FirstOrDefault(i => i.InterfaceIdentifier.Id == datapointTask.Info.HwInterfaceSchemeID);
            if(hwInterface == null)
                return new SGetCloudMapperCommandDefinition[0];
            return hwInterface.GetCommandsFor(datapointTask);
        }
    }
}