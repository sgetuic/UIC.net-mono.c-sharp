using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.I2c.Adafruit.VCNL4010
{
    public class Vcnl4010Edm :EmbeddedDriverModule
    {
        private readonly ILoggerFactory _loggerFactory;
        private readonly EdmCapability _edmCapability;
        private DatapointDefinition _sgetProximityDefinition;
        private DatapointDefinition _sgetAmbientDefinition;

        private EapiI2cEdm _eapiI2CEdm;
        private Vcnl4010Driver _vcnl4010Driver;
        
        public EdmIdentifier Identifier { get; }

        public Vcnl4010Edm(ILoggerFactory loggerFactory) {
            _loggerFactory = loggerFactory;
            Identifier = new Vcnl4010Identifier();
            _edmCapability = CreateEdmCapability();


            _edmCapability = CreateEdmCapability();
        }

        private EdmCapability CreateEdmCapability() {

            _sgetProximityDefinition = new SgetDatapointDefinition(new Guid("38910527-F62E-4EB3-8874-89A9AA2C07A8"), GetType().FullName + ".datapoint.proximity", UicDataType.Integer, "Proximity", string.Empty);
            _sgetAmbientDefinition = new SgetDatapointDefinition(new Guid("0D11024B-2133-4CAA-9E37-A6F22BCF12C0"), GetType().FullName + ".datapoint.ambient", UicDataType.Integer, "Ambient", string.Empty);
            List<DatapointDefinition> datapointDefinitions = new List<DatapointDefinition>();
            datapointDefinitions.Add(_sgetProximityDefinition);
            datapointDefinitions.Add(_sgetAmbientDefinition);
            
            return new Vlnc4010EdmCapability(Identifier, new CommandDefinition[0], new AttributeDefinition[0], datapointDefinitions.ToArray());
        }

        public void Initialize() {
            _eapiI2CEdm = new EapiI2cEdm(_loggerFactory);
            _eapiI2CEdm.Initialize();
            _vcnl4010Driver = new Vcnl4010Driver(_eapiI2CEdm.GetDriver(this), _loggerFactory.GetLoggerFor(typeof(Vcnl4010Driver)));

        }

        public void Dispose() {
            _eapiI2CEdm?.Dispose();
        }

        public EdmCapability GetCapability() {
            return _edmCapability;
        }

        public DatapointValue GetValueFor(DatapointDefinition datapoint) {
            if (datapoint.Id == _sgetAmbientDefinition.Id) {
                int ambient = _vcnl4010Driver.Adafruit_VCNL4010_ReadAmbient();
                return new SgetDatapointValue(ambient, datapoint);
            }
            if (datapoint.Id == _sgetProximityDefinition.Id) {
                int proximity = _vcnl4010Driver.Adafruit_VCNL4010_ReadProximity();
                return new SgetDatapointValue(proximity, datapoint);
            }
            
            throw new Exception("no definiton found for " + datapoint);
        }

        public AttributeValue GetValueFor(AttributeDefinition attribute) {
            throw new NotImplementedException();
        }

        public bool Handle(Command command) {
            // no commands to handle
            return false;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            // nothing to do yet
        }

        public void SetAttributeCallback(AttributeDefinition attributeDefinition, Action<AttributeValue> callback) {
            // nothing to do yet
        }
    }
}
