using System;
using System.Collections.Generic;
using UIC.EDM.EApi.BoardInformation.EApi;
using UIC.EDM.EApi.BoardInformation.EApi.BoardInformation;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framweork.DefaultImplementation;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdm : EmbeddedDriverModule
    {
        private readonly BoardInformationDriver _boardInformationDriver;
        private readonly EapiInitializer _eapiInitializer;
        private readonly EapiBoardInformationEdmCapabilityProvider _eapiBoardInformationEdmCapabilityProvider;

        public Edmldentifier Identifier { get; }
        

        public EapiBoardInformationEdm() {
            Identifier = new EapiBoardInformationEdmIdentifier();
            _eapiInitializer = new EapiInitializer();
            _boardInformationDriver = new BoardInformationDriver();

            _eapiBoardInformationEdmCapabilityProvider = new EapiBoardInformationEdmCapabilityProvider(Identifier);
        }

        public void Initialize() {
            _eapiInitializer.Init();
        }

        public void Dispose() {
            _eapiInitializer.Dispose();
        }

        public EdmCapability GetCapability() {
            return _eapiBoardInformationEdmCapabilityProvider.EdmCapability;
        }

        public DatapointValue GetValueFor(DatapointDefinition datapoint) {

            object value;
            if (_eapiBoardInformationEdmCapabilityProvider.TryGet(datapoint.Id, out BoardInformationValueId valueId)) {
                uint boardInformationValue = _boardInformationDriver.GetBoardInformationOf(valueId);
                value = boardInformationValue;
            }
            else {
                throw new ArgumentException("Unknown datapoitn defintion id: " + datapoint.Id);
            }
            return new SgetDatapointValue(value, datapoint);
        }

        public AttributeValue GetValueFor(AttribtueDefinition attribtue) {
            object value;
            if (_eapiBoardInformationEdmCapabilityProvider.TryGet(attribtue.Id, out BoardInformationValueId valueId))
            {
                uint boardInformationOf = _boardInformationDriver.GetBoardInformationOf(valueId);
                value = boardInformationOf;
            }
            else if (_eapiBoardInformationEdmCapabilityProvider.TryGet(attribtue.Id, out BoardInformationStringId stringId))
            {
                string boardInformationOf = _boardInformationDriver.GetBoardInformationOf(stringId);
                value = boardInformationOf;
            }
            else
            {
                throw new ArgumentException($"Unknown {nameof(AttribtueDefinition)} id: " + attribtue.Id);
            }
            return new SgetAttributeValue(value, attribtue);
        }

        public bool Handle(Command command) {
            return false;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            // no need for callbacks
        }

        public void SetAttributeCallback(AttribtueDefinition attribtueDefinition, Action<AttributeValue> callback) {
            // no need for callbacks

        }
    }
}
