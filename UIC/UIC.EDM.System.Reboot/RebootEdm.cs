using System;
using System.Collections.Generic;
using System.Diagnostics;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdm : EmbeddedDriverModule
    {
        private readonly EdmCapability _edmCapability;
        private readonly SgetCommandDefinition _rebootCommnand;
        public Edmldentifier Identifier { get; }

        public RebootEdm() {
            Identifier = new RebootEdmldentifier();
            _rebootCommnand = new SgetCommandDefinition("Reboot System", "reboot", UicDataType.String, "Reboots the system", null, null);
            IEnumerable<CommandDefinition> commandDefinitios = new[] {
                _rebootCommnand
            };
            IEnumerable<AttributeDefinition> attribtueDefinitions = new SgetAttributDefinition[0];
            IEnumerable<DatapointDefinition> datapointDefinitions = new SgetDatapointDefinition[0];
            _edmCapability = new RebootEdmEdmCapability(Identifier, commandDefinitios, attribtueDefinitions, datapointDefinitions);
        }

        public void Initialize() {
            
        }

        public void Dispose() {
            
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
