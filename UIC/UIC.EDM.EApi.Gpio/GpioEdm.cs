using System;
using System.Collections.Generic;
using System.Threading;
using UIC.EDM.EApi.Gpio.Eapi;
using UIC.EDM.EApi.Shared;
using UIC.Framework.Interfaces.Edm;
using UIC.Framework.Interfaces.Edm.Definition;
using UIC.Framework.Interfaces.Edm.Value;
using UIC.Framework.Interfaces.Project;
using UIC.Framework.Interfaces.Util;
using UIC.Framweork.DefaultImplementation;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.Gpio
{
    public class GpioEdm :EmbeddedDriverModule
    {
        private EdmCapability _edmCapability;
        private readonly GpioDriver _gpioDriver;
        private readonly EapiInitializer _eapiInitializer;
        private readonly ILogger _logger;

        private readonly Dictionary<string, EapiGpioId> _gpioIdMap = new Dictionary<string, EapiGpioId>();
        private readonly Dictionary<string, EapiGpioId> _offCommandMap = new Dictionary<string, EapiGpioId>();
        private readonly Dictionary<string, EapiGpioId> _onCommandMap = new Dictionary<string, EapiGpioId>();
        private Action<DatapointValue> _action;

        public EdmIdentifier Identifier { get; }

        public GpioEdm(ILoggerFactory loggerFactory) {
            Identifier = new GpioEdmIdentifier();
            _logger = loggerFactory.GetLoggerFor(GetType());
            
            _gpioDriver = new GpioDriver(loggerFactory.GetLoggerFor(typeof(GpioDriver)), new EApiStatusCodes());
            _eapiInitializer = new EapiInitializer();
        }

        

        public void Initialize() {
            _eapiInitializer.Init();
            var gpioCapabilities = _gpioDriver.GetGpioCapabilities();
            _edmCapability = CreateEdmCapability();
            Test(gpioCapabilities);
        }

        private EdmCapability CreateEdmCapability() {
            var dataPoints = new List<DatapointDefinition>();
            var commands = new List<CommandDefinition>();
            foreach (EapiGpioId pin in Enum.GetValues(typeof(EapiGpioId)))
            {
                string pinname = "Pin" + (int)pin;
                string uri = UicUriBuilder.DatapointFrom(this, pinname);
                _gpioIdMap[uri] = pin;

                var pinDef = new SgetDatapointDefinition(pin.ToGuid(), uri, UicDataType.Bool, pinname, string.Empty);
                dataPoints.Add(pinDef);


                if ((int)pin > (int)EapiGpioId.EAPI_ID_GPIO_GPIO07)
                {
                    var onCommand = new SgetCommandDefinition(
                        pin.ToOnCommandGuid(), 
                        UicUriBuilder.CommandFrom(this, pinname+".on"), 
                        "Set " + pinname, 
                        pinname + "@set", 
                        UicDataType.Bool, 
                        string.Empty, 
                        pinDef
                        , new[] { "On" });
                    commands.Add(onCommand);
                    _onCommandMap.Add(onCommand.Uri, pin);
                    var offCommand = new SgetCommandDefinition(
                        pin.ToOffCommandGuid(),
                        UicUriBuilder.CommandFrom(this, pinname + ".off"),
                        "Reset " + pinname,
                        pinname + "@reset",
                        UicDataType.Bool,
                        string.Empty,
                        pinDef
                        , new[] {"Off"});
                    commands.Add(offCommand);
                    _offCommandMap.Add(offCommand.Uri, pin);
                }
            }
            
            return new GpioEdmCapability(Identifier, dataPoints.ToArray(), new AttributeDefinition[0], commands.ToArray());
        }

        public void Dispose() {
           
        }

        public EdmCapability GetCapability() {
            return _edmCapability;
        }

        public DatapointValue GetValueFor(DatapointDefinition datapoint) {
            var eapiGpioId = _gpioIdMap[datapoint.Uri];
            GpioLevel gpioLevel = _gpioDriver.GetLevel();
            var levelOf = gpioLevel.GetLevelOf(eapiGpioId);
            return new SgetDatapointValue(levelOf, datapoint);
        }

        public AttributeValue GetValueFor(AttributeDefinition attribute) {
            throw new NotImplementedException();
        }

        public bool Handle(Command command) {
            EapiGpioId pin;
            if (_onCommandMap.TryGetValue(command.CommandDefinition.Uri, out pin)) {
                _gpioDriver.SetLevel(pin, GpioLevelEnum.EapiGpioHigh);
                var gpioLevel = _gpioDriver.GetLevel();
                _action(new SgetDatapointValue(gpioLevel.GetLevelOf(pin), command.CommandDefinition.RelatedDatapoint));
                return true;
            }
            if (_offCommandMap.TryGetValue(command.CommandDefinition.Uri, out pin)) {
                _gpioDriver.SetLevel(pin, GpioLevelEnum.EapiGpioLow);
                var gpioLevel = _gpioDriver.GetLevel();
                _action(new SgetDatapointValue(gpioLevel.GetLevelOf(pin), command.CommandDefinition.RelatedDatapoint));
                return true;
            }

            return false;
        }

        public void SetDatapointCallback(ProjectDatapointTask datapointTask, Action<DatapointValue> callback) {
            _action = callback;
        }

        public void SetAttributeCallback(AttributeDefinition attributeDefinition, Action<AttributeValue> callback) {
            // nothing to do yet
        }

        private void Test(GpioCapability gpioCap)
        {
            if (gpioCap.IsUnsupported)
            {
                _logger.Warning("GPIO is not supported");
                return;
            }

            _logger.Information(gpioCap.ToString());
            GpioLevel level = _gpioDriver.GetLevel();
            _logger.Information(level.ToString());

            _logger.Information("EapiGpioId Value:" + _gpioDriver.GetLevel().GetLevelOf(EapiGpioId.EAPI_ID_GPIO_GPIO14));

            for (int i = 0; i < 16; i++)
            {
                EapiGpioId pin = (EapiGpioId)i;
                if (gpioCap.IsOutput(pin))
                {
                    _gpioDriver.SetLevel(pin, GpioLevelEnum.EapiGpioHigh);
                }
                Thread.Sleep(200);
            }

            for (int i = 0; i < 16; i++)
            {
                EapiGpioId pin = (EapiGpioId)i;
                if (gpioCap.IsOutput(pin))
                {
                    _gpioDriver.SetLevel(pin, GpioLevelEnum.EapiGpioLow);
                }
                Thread.Sleep(200);
            }

            _logger.Information("EapiGpioId Value:" + _gpioDriver.GetLevel().GetLevelOf(EapiGpioId.EAPI_ID_GPIO_GPIO14));
            _gpioDriver.SetAll(GpioLevelEnum.EapiGpioHigh);
        }
    }
}
