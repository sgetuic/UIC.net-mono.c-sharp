namespace UIC.Framework.Interfaces.Configuration
{
    public interface UicConfiguartion
    {
        string ProjectKey { get; }
        bool IsEdmSnychronizationEnabled { get; }
        string AbsoluteProjectConfigurationFilePath { get; }
        bool IsRemoteProjectLoadingEnabled { get; }
    }
}
