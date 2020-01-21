//-----------------------------------------------------------------------
// <copyright file="AzureProjectAgent.cs" company="Kontron Europe GmbH">
//     Copyright (c) Kontron Europe GmbH. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

using System;
using System.IO;
using System.Net;
using System.Text;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Project;
using UIC.Util;
using UIC.Util.Extensions;
using UIC.Util.Logging;
using UIC.Util.Serialization;
using UIC.Framweork.DefaultImplementation;

namespace UIC.Communication.Azure.ProjectAgent
{
    public class AzureProjectAgent : Framework.Interfaces.Communication.Projects.ProjectAgent
    {
        private readonly ILogger _logger;
        private readonly ILoggerFactory _loggerFactory;
        private readonly AzureProjectAgentConfiguration _configuration;
        private readonly ISerializer _serializer;


        public AzureProjectAgent(ISerializer serializer, ILoggerFactory loggerFactory)
        {
            _loggerFactory = loggerFactory;
            _logger = loggerFactory.GetLoggerFor(GetType());
            _configuration = GetConfiguration(serializer);
            _serializer = serializer;
        }


        public void Synchronize(EdmCapability edmCapability)
        {
        }

        public UicProject LoadProject(UicConfiguartion uicConfiguartion)
        {
            _logger.Information("LoadProjectConfiguration for " + uicConfiguartion.ProjectKey);
            string url = String.Format("{0}/project-{1}.json", _configuration.AzureStorageConnectionString, uicConfiguartion.ProjectKey);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "GET";
            string rawData = ExecuteRequest(request, string.Empty, _logger);
            var sgetProject = _serializer.Deserialize<SgetUicProject>(rawData);
            return sgetProject;
        }

        private AzureProjectAgentConfiguration GetConfiguration(ISerializer serializer)
        {
            var configHandler = new ConfigurationJsonFileHandler(@".\azure_project_agent.json", serializer, _logger);
            AzureProjectAgentConfiguration config;
            if (configHandler.IsConfigFileExisting())
            {
                config = configHandler.Load<AzureProjectAgentConfiguration>();
            }
            else
            {
                config = new AzureProjectAgentConfiguration();
                configHandler.Backup(config);
            }
            return config;
        }

        public void Initialize(EmbeddedDriverModule[] modules)
        {
        }

        internal string ExecuteRequest(HttpWebRequest request, string payload, ILogger logger)
        {
            request.Accept = "*/*";
            request.ServerCertificateValidationCallback = (sender, certificate, chain, errors) =>
            {
                logger.Information("accept server certificate");
                return true;
            };
            if (request.Method != "GET")
            {
                if (payload.IsNullOrEmpty())
                {
                    request.ContentLength = 0;
                }
                else
                {
                    var data = Encoding.UTF8.GetBytes(payload);
                    var reqStr = request.GetRequestStream();
                    reqStr.Write(data, 0, data.Length);
                    request.ContentType = "application/json";
                }
            }

            logger.Information(request.RequestUri.ToString());

            using (HttpWebResponse resp = (HttpWebResponse)request.GetResponse())
            {
                return ReadDataFrom(resp);
            }
        }

        internal static string ReadDataFrom(HttpWebResponse resp)
        {
            string rawData = String.Empty;
            if (resp.ContentLength > 0)
            {
                using (var s = new StreamReader(resp.GetResponseStream()))
                {
                    rawData = s.ReadToEnd();
                }
            }

            if (resp.StatusCode == HttpStatusCode.NotFound)
            {
                return String.Empty;
            }

            if (resp.StatusCode != HttpStatusCode.OK)
            {
                throw new Exception(resp.StatusCode + ": " + rawData);
            }

            return rawData;
        }

    }
}
