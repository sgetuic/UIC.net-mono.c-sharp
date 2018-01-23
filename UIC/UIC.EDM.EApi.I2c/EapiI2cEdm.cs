using System;
using System.Collections.Generic;
using UIC.EDM.EApi.I2c.EApi;
using UIC.EDM.EApi.I2c.EApi.i2c;
using UIC.EDM.EApi.Shared;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.I2c
{
    public class EapiI2cEdm : EmbeddedDriverModule
    {
        private readonly I2cDriver _i2CDriver;
        private readonly EapiInitializer _eapiInitializer;
        private readonly EdmCapability _edmCapability;
        private readonly Guid _maxblockLengthAttributeId = new Guid("{16c89a68-a270-489b-affb-2f9c1c43902e}");
        private EmbeddedDriverModule _owner;

        public EdmIdentifier Identifier { get; }
        

        public EapiI2cEdm(ILoggerFactory loggerFactory) {
            Identifier = new EapiI2cEdmIdentifier(GetType().FullName);
            _eapiInitializer = new EapiInitializer();
            _i2CDriver = new I2cDriver(loggerFactory.GetLoggerFor(typeof(I2cDriver)), new EApiStatusCodes());

            _edmCapability = CreateEdmCapability();
        }

        private EdmCapability CreateEdmCapability() {
            List<AttributeDefinition> attribtueDefinitions = new List<AttributeDefinition>();
            attribtueDefinitions.Add(
                new SgetAttributDefinition(
                    _maxblockLengthAttributeId,
                    UicUriBuilder.DatapointFrom(this, "MaxBlockLength"),
                    "I2C MaxBlockLength",
                    UicDataType.Integer,
                    string.Empty));
            return new EapI2cEdmCapability(Identifier, new CommandDefinition[0], attribtueDefinitions.ToArray(), new DatapointDefinition[0]);
        }

        public void Initialize() {
            _eapiInitializer.Init();
        }

        public void Dispose() {
            _eapiInitializer.Dispose();
        }

        public EdmCapability GetCapability() {
            return _edmCapability;
        }

        public DatapointValue GetValueFor(DatapointDefinition datapoint) {
            throw new ArgumentException("Unknown DatapointDefinition: " + datapoint);
        }

        public AttributeValue GetValueFor(AttributeDefinition attribute) {
            if (attribute.Id == _maxblockLengthAttributeId) {
                var i2CBusCapability = _i2CDriver.GetBusCapabilities();
                return new SgetAttributeValue(i2CBusCapability.MaxBlockLen, attribute);
            }
            throw new ArgumentException("Unknown AttributeDefinition: " + attribute);
        }

        public bool Handle(Command command) {
            return false;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            // no need for callbacks
        }

        public void SetAttributeCallback(AttributeDefinition attributeDefinition, Action<AttributeValue> callback) {
            // no need for callbacks

        }

        internal I2cDriver GetDriver(EmbeddedDriverModule edm) {
            if(_owner != null && _owner != edm) throw new Exception($"{Identifier.Uri} is already in use by {edm.Identifier.Uri}");
            _owner = edm;
            return _i2CDriver;
        }
    }
}
