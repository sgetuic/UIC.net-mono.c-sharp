using System;
using UIC.Framework.Interfaces.Project;

namespace UIC.Communication.M2mgo.CommunicationAgent.Translation.Project {
    internal class M2MgoProjectTranslator {
        internal Guid GetProjectDomain(UicProject project)
        {
            return project.CustomerForeignKey;
        }
    }
}
