using UIC.Framework.Interfaces.Configuration;

namespace UIC.SGeT.Launcher
{
    class SGeTUicConfiguartion : UicConfiguartion
    {
        public bool IsEdmSnychronizationEnabled
        {
            get; set;
        }

        public bool IsRemoteProjectLoadingEnabled
        {
            get; set;
        }

        public string ProjectJsonFilePath
        {
            get; set;
        }

        public string ProjectKey
        {
            get; set;
        }

        public string CommunicationAgent
        {
            get; set;
        }
    }
}
