using System;
using System.Collections.Generic;
using System.Linq;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.Attribute;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface.DataPoint;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule
{
    public class EmbeddedDriverModulCapabilitiy
    {
        public EmbeddedModuleIdentifier Identifier { get; private set; }
        public IEnumerable<EmbeddedModuleHwInterfaceCapability> Interfaces { get; private set; }

        

        public EmbeddedDriverModulCapabilitiy(EmbeddedModuleIdentifier embeddedModuleIdentifier, IEnumerable<EmbeddedModuleHwInterfaceCapability> interfaces)
        {
            if (interfaces == null) throw new ArgumentNullException(nameof(interfaces));
            
            Identifier = embeddedModuleIdentifier;
            Interfaces = interfaces;

            foreach (EmbeddedModuleHwInterfaceCapability @interface in Interfaces)
            {
                var interfaceAttributeMap = @interface.Attributes.ToDictionary(a => a.Key, a => a);
        
                var interfaceDatapointMap = @interface.DataPoints.ToDictionary(a => a.Key, a => a);
            }
        }
    }
}