using System;
using System.Collections.Generic;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.EDM.Test.Mockup
{
    public class MockupEdm : EmbeddedDriverModule
    {
        private readonly ILogger _logger;
        private readonly MockupEdmEdmCapability _edmCapability;
        private readonly MockupValueProvider _mockupValueProvider;
        private Action<DatapointValue> _callback;
        private static readonly Guid CommandOnId = new Guid("{4129a33b-3f65-4169-92c8-95c1942a32f8}");
        private static readonly Guid CommandOffId = new Guid("{7f72d80d-43f8-49ae-a808-5f6026960c49}");

        public EdmIdentifier Identifier { get; }

        public MockupEdm(ILoggerFactory loggerFactory) {
            _logger = loggerFactory.GetLoggerFor(GetType());
            _mockupValueProvider = new MockupValueProvider();
            Identifier = new MockupEdmIdentifier(GetType().FullName);
            AttributeDefinition[] attribtueDefinitions = ConstructAttributes();
            DatapointDefinition boolDatapointMockup;
            DatapointDefinition[] datapointDefinitions = ConstructDatapoints(out boolDatapointMockup);
            CommandDefinition[] commandDefinitios = ConstructCommandsForBoolDatapoint(boolDatapointMockup);
            _edmCapability = new MockupEdmEdmCapability(Identifier, commandDefinitios, attribtueDefinitions, datapointDefinitions);
        }

        private CommandDefinition[] ConstructCommandsForBoolDatapoint(DatapointDefinition boDatapointDefinition) {
            var commands = new List<CommandDefinition>();
            commands.Add(new SgetCommandDefinition(CommandOnId, UicUriBuilder.CommandFrom(this, "SWITCH_ON"), "Switch On", "1", UicDataType.Bool, string.Empty, boDatapointDefinition, new []{"On"}));
            commands.Add(new SgetCommandDefinition(CommandOffId, UicUriBuilder.CommandFrom(this, "SWITCH_OFF"), "Switch Off", "0", UicDataType.Bool, string.Empty, boDatapointDefinition, new []{"Off"}));
            return commands.ToArray();
        }

        private DatapointDefinition[] ConstructDatapoints(out DatapointDefinition boolDatapoint) {
            var datapoints = new List<DatapointDefinition>();
            boolDatapoint = new SgetDatapointDefinition(new Guid("{83f02bea-c22b-46aa-b1c2-4ab8102d8a80}"), UicUriBuilder.DatapointFrom(this, "Bool_mock"), UicDataType.Bool, "Random Bool", "Digital input mockup");
            datapoints.Add(boolDatapoint);
            datapoints.Add(new SgetDatapointDefinition(new Guid("{4087d40d-d4e2-42b1-89a4-9b9d18499a04}"), UicUriBuilder.DatapointFrom(this, "Integer_mock"), UicDataType.Integer, "Random Integer", "Integer measurement mockup"));
            datapoints.Add(new SgetDatapointDefinition(new Guid("{a41fc3af-4f73-42bf-8290-43ed883edd8f}"), UicUriBuilder.DatapointFrom(this, "Double_mock"), UicDataType.Double, "Random Double", "Double measurement mockup"));
            datapoints.Add(new SgetDatapointDefinition(new Guid("{3b20829f-cc30-4923-a2d6-30502ccb9acd}"), UicUriBuilder.DatapointFrom(this, "Gps_mock"), UicDataType.Gps, "Random GPS", "geo location mockup"));
            datapoints.Add(new SgetDatapointDefinition(new Guid("{fbd3e390-ffb7-455b-b0dc-695b13329eb6}"), UicUriBuilder.DatapointFrom(this, "String_mock"), UicDataType.String, "Random String", "messaging mockup"));

            return datapoints.ToArray();
        }

        private AttributeDefinition[] ConstructAttributes() {
            var attributes = new List<AttributeDefinition>();
            attributes.Add(new SgetAttributDefinition(new Guid("(b68df3f9-4748-4c9d-9bda-567c87fab855)"), UicUriBuilder.AttributeFrom(this, "timestamp"), "Timestamp", UicDataType.String, "Simple DateTime string"));

            return attributes.ToArray();
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
            if(!datapoint.Uri.StartsWith(Identifier.Uri)) throw new ApplicationException($"Uri missmatch from datapoint {datapoint.Uri} and EDM {Identifier.Uri}");
            var value = _mockupValueProvider.GetRandomValueOf(datapoint.DataType);
            return new SgetDatapointValue(value, datapoint);
        }

        public AttributeValue GetValueFor(AttributeDefinition attribute) {
            if(!attribute.Uri.StartsWith(Identifier.Uri)) throw new ApplicationException($"Uri missmatch from attribute {attribute.Uri} and EDM {Identifier.Uri}");
            var value = _mockupValueProvider.GetRandomValueOf(attribute.DataType);
            return new SgetAttributeValue(value, attribute);
        }

        public bool Handle(Command command) {
            if(!command.CommandDefinition.Uri.StartsWith(Identifier.Uri)) throw new ApplicationException($"Uri missmatch from command {command.CommandDefinition.Uri} and EDM {Identifier.Uri}");

            if (command.CommandDefinition.Id == CommandOnId) {
                _callback(new SgetDatapointValue("1", command.CommandDefinition.RelatedDatapoint));
            }
            else if (command.CommandDefinition.Id == CommandOffId) {
                _callback(new SgetDatapointValue("0", command.CommandDefinition.RelatedDatapoint));
            }
            else {
                return false;
            }
            return true;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            _callback = callback;
        }

        public void SetAttributeCallback(AttributeDefinition attributeDefinition, Action<AttributeValue> callback) {
            // no need for callbacks

        }

        
    }
}
