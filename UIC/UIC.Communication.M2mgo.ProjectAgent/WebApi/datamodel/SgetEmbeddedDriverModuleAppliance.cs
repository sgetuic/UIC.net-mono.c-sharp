using System;
using System.Collections.Generic;
using UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule;

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

    }
}
