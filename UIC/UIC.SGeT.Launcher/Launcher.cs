using System;
using System.Collections.Generic;
using UIC.Communication.M2mgo.CommunicationAgent;
using UIC.Communication.M2mgo.ProjectAgent;
using UIC.EDM.System.Reboot;
using UIC.EDM.Test.Mockup;
using UIC.Framework.Interfaces;
using UIC.Framework.Interfaces.Communication.Application;
using UIC.Framework.Interfaces.Communication.Projects;
using UIC.Framework.Interfaces.Configuration;
using UIC.Framework.Interfaces.Edm;
using UIC.SGET.ConnectorImplementation;
using UIC.Util;
using UIC.Util.Logging;
using UIC.Util.Serialization;

namespace UIC.SGeT.Launcher
{
    public class Launcher
    {
        private static ILogger _logger;

        static void Main() {
            UniversalIotConnector uic = null;
            try {
                ILoggerFactory loggerFactory = new NlogLoggerFactory();
                _logger = loggerFactory.GetLoggerFor(typeof(Launcher));

                ISerializer serializer = new UicSerializer();

                UicConfiguartion uicConfiguartion = GetConfiguration(serializer);
                List<EmbeddedDriverModule> embeddedDriverModules = GetEdms(loggerFactory);
                CommunicationAgent communicationAgent = new M2mgoCommunicationAgentImpl(serializer, loggerFactory);
                
                ProjectAgent projectAgent = new M2mgoProjectAgent(serializer, loggerFactory, embeddedDriverModules);
                uic = new SgetUniversalIotConnector(uicConfiguartion, embeddedDriverModules, communicationAgent, projectAgent, serializer, loggerFactory);
                uic.Initialize();

                _logger.Information("Enter to Dispose ....");
                Console.ReadLine();
            }
            catch (Exception e) {
                _logger.Error(e);
            }
            finally
            {
                if (uic != null)
                {
                    _logger.Information("Dipose uic ");
                    try {
                        uic.Dispose();
                    } catch (Exception e) {
                        _logger.Error(e);
                    }    
                }
                
            }

            _logger.Information("Enter to end ....");
            Console.ReadLine();
        }

        private static List<EmbeddedDriverModule> GetEdms(ILoggerFactory loggerFactory) {
            return new List<EmbeddedDriverModule> {
                new RebootEdm(loggerFactory),
                new MockupEdm(loggerFactory),
            };
        }

        private static UicConfiguartion GetConfiguration(ISerializer serializer)
        {
            var configHandler = new ConfigurationJsonFileHandler(@".\cloudmapper.json", serializer, _logger);
            UicConfiguartion config;
            if (configHandler.IsConfigFileExisting())
            {
                config = configHandler.Load<UicConfiguartion>();
            }
            else
            {
                config = new PstUicConfiguartion();
                //config = new LocalhostConfiguration();
                configHandler.Backup(config);
            }
            return config;
        }

        
    }
}
