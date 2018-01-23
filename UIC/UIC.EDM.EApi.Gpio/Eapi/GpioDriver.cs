using System;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using UIC.EDM.EApi.Shared;
using UIC.Util.Extensions;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.Gpio.Eapi
{
     internal class GpioDriver {
        private readonly ILogger _logger;
        private readonly EApiStatusCodes _eApiStatusCodes;
        
        private const uint BitmaskFullGpioVector = 0xffff;

        private readonly uint _eapiIdGpioBank00 = EAPI_GPIO_BANK_ID(0); /* GPIOs  0 - 31 (optional) */
        private readonly uint _eapiIdGpioBank01 = EAPI_GPIO_BANK_ID(32); /* GPIOs 32 - 63 (optional) */
        private readonly uint _eapiIdGpioBank02 = EAPI_GPIO_BANK_ID(64); /* GPIOs 64 - 95 (optional) */
        private GpioCapability _gpioCapability;

        public GpioDriver(ILogger logger, EApiStatusCodes eApiStatusCodes) 
        {
            _logger = logger;
            _eApiStatusCodes = eApiStatusCodes;
        }

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiGPIOGetDirectionCaps(UInt32 id, ref UInt32 inputs, ref UInt32 outputs);
        
        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiGPIOGetDirection(UInt32 id, UInt32 bitmask, ref UInt32 directions);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiGPIOSetDirection(UInt32 id, UInt32 bitmask, UInt32 directions);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiGPIOGetLevel(UInt32 id, UInt32 bitmask, ref UInt32 level);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiGPIOSetLevel(UInt32 id, UInt32 bitmask, UInt32 level);

        
        /*
         * Multiple GPIOs Per ID Mapping
         */

        private static uint EAPI_GPIO_BANK_ID(uint bankId)
        {
            return (0x10000 | ((bankId) >> 5));
        }


        //private static uint EAPI_GPIO_BANK_MASK(uint GPIO_NUM)
        //{
        //    return (uint) (1 << (int)((GPIO_NUM) & 0x1F));
        //}
        //#define EAPI_GPIO_BANK_TEST_STATE(GPIO_NUM, TState, TValue) \ (((TValue>>((GPIO_NUM)&0x1F))&1)==(TState))

        internal GpioCapability GetGpioCapabilities()
        {
            uint inputCapabilityMask = 0;
            uint outputCapabilityMask = 0;
            bool isUnsupported = false;
            
            if (_gpioCapability != null) return _gpioCapability;
        
            uint resultCode = EApiGPIOGetDirectionCaps(_eapiIdGpioBank00, ref inputCapabilityMask, ref outputCapabilityMask);
            
            if (_eApiStatusCodes.IsUnsupported(resultCode)) {
                _logger.Warning(_eapiIdGpioBank00 + " EApiGPIOGetDirectionCaps is unsupported");
                isUnsupported = true;
            }
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                _logger.Warning("EApiGPIOGetDirectionCaps: " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
                isUnsupported = true;
            }

            _gpioCapability = new GpioCapability(inputCapabilityMask, outputCapabilityMask, isUnsupported);
            _logger.Information("EApiGPIOGetDirectionCaps: " + _gpioCapability);
            return _gpioCapability;
        }

        internal void SetDirection(EapiGpioId id, EapiGpioDirectionEnum directionEnum) {
            throw new NotImplementedException("SetDirection");
        }

        public GpioLevel GetLevel()
        {
            uint level = ReadGpioRegister();
            var gpioLevel = new GpioLevel(level);
            return gpioLevel;
        }

        private uint ReadGpioRegister()
        {
            GpioCapability gpioCapabilities = GetGpioCapabilities();
            if (gpioCapabilities.IsUnsupported)
                return 0;

            uint bitmask = gpioCapabilities.InputCapabilityMask | gpioCapabilities.OutputCapabilityMask;
            uint level = 0;
            uint resultCode = EApiGPIOGetLevel(_eapiIdGpioBank00, bitmask, ref level);

            if (!_eApiStatusCodes.IsSuccess(resultCode))
            {
                throw new Exception("EApiGPIOGetLevel: " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
            _logger.Information("GetLevel: id={0}, bitmask={1}, levelEnum={2}", _eapiIdGpioBank00, bitmask.ToBinaryString(), level.ToBinaryString());
            return level;
        }

        public void SetLevel(EapiGpioId id, GpioLevelEnum levelEnum) {
            GpioCapability gpioCapabilities = GetGpioCapabilities();
            if (!gpioCapabilities.IsOutput(id))
            {
                throw new Exception(id + " is not an output");
            }
            uint bitmask = (uint) (gpioCapabilities.OutputCapabilityMask & (1 << (int)id));

            int newLevelVAlues = (int)ReadGpioRegister();
            if (levelEnum == GpioLevelEnum.EapiGpioHigh) {
                newLevelVAlues |= 1 << (int)id;
            } else {
                newLevelVAlues &= ~(1 << (int)id);
            }

            _logger.Information("SetLevel {0} to {1} =>  bitmask={2}, newLevelVAlues={3}", id, levelEnum, Convert.ToString(bitmask, 2), Convert.ToString(newLevelVAlues, 2));


            uint resultCode = EApiGPIOSetLevel(_eapiIdGpioBank00, bitmask, (uint)(0xffff & newLevelVAlues));
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception("EApiGPIOSetLevel: " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
        }

        public void SetAll(GpioLevelEnum levelEnum) {
            GpioCapability gpioCapabilities = GetGpioCapabilities();
            uint bitmask = gpioCapabilities.OutputCapabilityMask;

            uint newLevelValues = (uint) (levelEnum == GpioLevelEnum.EapiGpioHigh ? 0xffff : 0);

            _logger.Information("SetAll: id={0}, bitmask={1}, new level={2}", _eapiIdGpioBank00, bitmask.ToBinaryString(), newLevelValues.ToBinaryString());

            uint resultCode = EApiGPIOSetLevel(_eapiIdGpioBank00, bitmask, newLevelValues);
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception("EApiGPIOSetLevel: " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
        }
    }
}
