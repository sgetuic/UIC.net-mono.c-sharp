using UIC.Framework.Interfaces.Configuration;

namespace UIC.SGeT.Launcher
{
    internal class PstUicConfiguartion : UicConfiguartion
    {
        public PstUicConfiguartion() {
            IsEdmSnychronizationEnabled = false;
            IsRemoteProjectLoadingEnabled = true;
            ProjectKey = "26895846c960465ebd89f28d10e6460c";
            AbsoluteProjectConfigurationFilePath = @".\project.json";
        }
        public string ProjectKey { get; }
        public bool IsEdmSnychronizationEnabled { get; }
        public string AbsoluteProjectConfigurationFilePath { get; }
        public bool IsRemoteProjectLoadingEnabled { get; }
    }
}