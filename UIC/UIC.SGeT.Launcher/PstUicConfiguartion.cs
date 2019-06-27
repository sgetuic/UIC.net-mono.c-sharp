using UIC.Framework.Interfaces.Configuration;

namespace UIC.SGeT.Launcher
{
    internal class PstUicConfiguartion : UicConfiguartion
    {
        public PstUicConfiguartion() {
            IsEdmSnychronizationEnabled = true;
            IsRemoteProjectLoadingEnabled = true;
            ProjectKey = "26895846c960465ebd89f28d10e6460c";
            ProjectJsonFilePath = @".\project.json";
            CommunicationAgent = "M2MGO";
        }
        public string ProjectKey { get; }
        public bool IsEdmSnychronizationEnabled { get; }
        public string ProjectJsonFilePath { get; }
        public bool IsRemoteProjectLoadingEnabled { get; }

        public string CommunicationAgent { get; }
    }
}