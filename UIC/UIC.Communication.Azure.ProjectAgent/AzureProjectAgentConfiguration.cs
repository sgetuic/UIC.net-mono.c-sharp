//-----------------------------------------------------------------------
// <copyright file="AzureProjectAgentConfiguration.cs" company="Kontron Europe GmbH">
//     Copyright (c) Kontron Europe GmbH. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace UIC.Communication.Azure.ProjectAgent
{
    public class AzureProjectAgentConfiguration
    {
        public string AzureStorageConnectionString { get; set; }

        public AzureProjectAgentConfiguration()
        {
            /// WARNING: customer must change it to the customer's Azure blob storage connection string
            /// or provide it in @".\azure_project_agent.json"
            AzureStorageConnectionString = "https://sgetuicprojects.blob.core.windows.net/projects";
        }
    }
}
