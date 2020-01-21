namespace UIC.Framework.Interfaces.Configuration
{
    public interface UicConfiguartion
    {
        string ProjectKey { get; }
        bool IsEdmSnychronizationEnabled { get; }
        string ProjectJsonFilePath { get; }
        bool IsRemoteProjectLoadingEnabled { get; }
        string CommunicationAgent { get; }
    }
}
