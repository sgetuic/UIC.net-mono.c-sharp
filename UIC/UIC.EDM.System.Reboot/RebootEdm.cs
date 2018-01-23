using System;
using System.Diagnostics;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdm : EmbeddedDriverModule
    {
        private readonly EdmCapability _edmCapability;
        private readonly SgetCommandDefinition _rebootCommnand;
        private ILogger _logger;
        public EdmIdentifier Identifier { get; }

        public RebootEdm(ILoggerFactory loggerFactory) {

            _logger = loggerFactory.GetLoggerFor(GetType());

            Identifier = new RebootEdmIdentifier(GetType().FullName);
            _rebootCommnand = new SgetCommandDefinition(new Guid("{f54b990d-25f5-430d-8428-44ab74ed8509}"), UicUriBuilder.CommandFrom(this, "reboot"), "Reboot System", "reboot", UicDataType.String, "Reboots the system", null, null);
            CommandDefinition[] commandDefinitios = {
                _rebootCommnand
            };
            AttributeDefinition[] attribtueDefinitions = new AttributeDefinition[0];
            DatapointDefinition[] datapointDefinitions = new DatapointDefinition[0];
            _edmCapability = new RebootEdmEdmCapability(Identifier, commandDefinitios, attribtueDefinitions, datapointDefinitions);
        }

        public void Initialize() {
            _logger.Information("Initialize");   
        }

        public void Dispose() {
            _logger.Information("Dispose");
        }

        public EdmCapability GetCapability() {
            return _edmCapability;
        }

        public DatapointValue GetValueFor(DatapointDefinition datapoint) {
            // no datapopints
            return null;
        }

        public AttributeValue GetValueFor(AttributeDefinition attribute) {
            // no attributes
            return null;
        }

        public bool Handle(Command command) {
            _logger.Information("Handle command: " + command.CommandDefinition.Uri);
            if (command.CommandDefinition.Id == _rebootCommnand.Id) {


                int p = (int)Environment.OSVersion.Platform;
                if ((p == 4) || (p == 6) || (p == 128))
                {
                    Console.WriteLine("Running on Unix");
                    Process.Start("reboot");
                }
                else
                {
                    Console.WriteLine("Probably running on windows");
                    ProcessStartInfo startinfo = new ProcessStartInfo("shutdown.exe", "-r");
                    Process.Start(startinfo);
                }
                return true;
            }
            return false;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            // no need for callbacks
        }

        public void SetAttributeCallback(AttributeDefinition attributeDefinition, Action<AttributeValue> callback) {
            // no need for callbacks

        }
    }
}
