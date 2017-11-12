using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Util;

namespace UIC.Framework.Interfaces.Configuration
{
    public interface UicConfiguartion
    {
        string ProjectKey { get; }

        IEnumerable<EmbeddedDriverModule> EmbeddedDriverModulesToLoad { get; }
        bool IsEdmSnychronizationEnabled { get; }
        Url EdmSnychronizationUrl { get; }

        string AbsoluteProjectConfigurationFilePath { get; }
        bool IsRemoteProjectLoadingEnabled { get; }
        Url RemoteProjectConfigurationUrl();
    }
}
