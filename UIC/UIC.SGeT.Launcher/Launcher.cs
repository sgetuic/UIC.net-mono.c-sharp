using System;
using System.Linq;
using System.Collections.Generic;
using UIC.Communication.M2mgo.CommunicationAgent;
using UIC.Communication.M2mgo.ProjectAgent;
using UIC.Communication.Azure.CommunicationAgent;
using UIC.EDM.EApi.BoardInformation;
using UIC.EDM.EApi.Gpio;
using UIC.EDM.EApi.I2c.Adafruit.VCNL4010;
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
//<<<<<<< HEAD
using UIC.Communication.Azure.ProjectAgent;
//using CommandLine;

//=======
using HAW.AWS.CommunicationAgent;
//>>>>>>> HAW-AWS

namespace UIC.SGeT.Launcher
{
    public class Launcher
    {
        private static ILogger _logger;
        private static ILoggerFactory loggerFactory;

//<<<<<<< HEAD
//        private static Options options;
//=======
        static void Main()
        {
            UniversalIotConnector uic = null;
            try
            {
                ILoggerFactory loggerFactory = new NlogLoggerFactory();
                _logger = loggerFactory.GetLoggerFor(typeof(Launcher));
                _logger.Information("Let's go");
//>>>>>>> HAW-AWS
/*
        private static void Start(Options opts)
        {
            options = opts;
            UniversalIotConnector uic = null;
            try
            {
                ISerializer serializer = new UicSerializer();

                // Azure project JSON demo:
                //PstUicProject pstProject = new PstUicProject(loggerFactory);
                //System.Console.Write(serializer.Serialize(pstProject, true));

                CommunicationAgent communicationAgent = null;
                ProjectAgent projectAgent = null;

                if ("m2mgo".Equals(options.AgentName.ToLower()))
                {
                    communicationAgent = new M2mgoCommunicationAgentImpl(serializer, loggerFactory);
                    projectAgent = new M2mgoProjectAgent(serializer, loggerFactory);
                }
                else if ("azure".Equals(options.AgentName.ToLower()))
                {
                    communicationAgent = new AzureCommunicationAgentImpl(serializer, loggerFactory);
                    projectAgent = new AzureProjectAgent(serializer, loggerFactory);
                }
                else
                {
                    throw new Exception("illegal agent name: " + options.AgentName);
                }*/
                ISerializer serializer = new UicSerializer();
                UicConfiguartion uicConfiguartion = GetConfiguration(serializer);
                List<EmbeddedDriverModule> embeddedDriverModules = GetEdms(loggerFactory);
                
                CommunicationAgent communicationAgent= null;
                ProjectAgent projectAgent = null;
                if (uicConfiguartion.CommunicationAgent == null)
                {
                    communicationAgent = new M2mgoCommunicationAgentImpl(serializer, loggerFactory);
                    _logger.Information("Used M2MGO Communication Agent as default");
                    projectAgent = new M2mgoProjectAgent(serializer, loggerFactory);
                }
                else
                {
                    if (uicConfiguartion.CommunicationAgent.Equals("AWS"))
                    {

                        communicationAgent = new HAWCommunicationAgent(serializer, loggerFactory);
                        _logger.Information("Used HAW Communication Agent");
                        projectAgent = new M2mgoProjectAgent(serializer, loggerFactory);
                    }

                    if (uicConfiguartion.CommunicationAgent.Equals("AZURE"))
                    {
                        communicationAgent = new AzureCommunicationAgentImpl(serializer, loggerFactory);
                        projectAgent = new AzureProjectAgent(serializer, loggerFactory);
                        _logger.Information("Used AZURE Communication Agent");
                    }
                    else
                    {
                        _logger.Information("no agent used");
                    }
                }

                uic = new SgetUniversalIotConnector(uicConfiguartion, communicationAgent, projectAgent, serializer, loggerFactory);

                uic.Initialize(embeddedDriverModules.ToArray());

                _logger.Information("Enter to Dispose ....");
                Console.ReadLine();
            }
            catch (Exception e)
            {
                _logger.Error(e);
            }
            finally
            {
                if (uic != null)
                {
                    _logger.Information("Dipose uic ");
                    try
                    {
                        uic.Dispose();
                    }
                    catch (Exception e)
                    {
                        _logger.Error(e);
                    }
                }

            }

            _logger.Information("Enter to end ....");
            Console.ReadLine();
        }


        private static List<EmbeddedDriverModule> GetEdms(ILoggerFactory loggerFactory)
        {
            return new List<EmbeddedDriverModule> {
                new RebootEdm(loggerFactory),
                new MockupEdm(loggerFactory),
                new GpioEdm(loggerFactory),
                new EapiBoardInformationEdm(),
                new Vcnl4010Edm(loggerFactory),
            };
        }

        private static UicConfiguartion GetConfiguration(ISerializer serializer)
        {
            var configHandler = new ConfigurationJsonFileHandler(@".\uic_config.json", serializer, _logger);
            UicConfiguartion config;
            if (configHandler.IsConfigFileExisting())
            {
                config = configHandler.Load<SGeTUicConfiguartion>();
            }
            else
            {
                config = new PstUicConfiguartion();
                configHandler.Backup(config);
            }
            return config;
        }


    }
}
