using System.Collections.Generic;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framework.Interfaces.Communication.Projects
{
    public interface ProjectAgent
    {
        void Synchronize(EdmCapability edmCapability);
        UicProject LoadProject(UicConfiguartion uicConfiguartion);
        void Initialize(EmbeddedDriverModule[] modules);
    }
}
