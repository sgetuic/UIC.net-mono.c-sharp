using System;
using System.Runtime.InteropServices;

namespace UIC.EDM.EApi.Gpio.Eapi {
    class EapiInitializer {
        private readonly EApiStatusCodes _eApiStatusCodes;

        [DllImport("Eapi_1.dll")]
        public static extern UInt32 EApiLibInitialize();

        [DllImport("Eapi_1.dll")]
        public static extern UInt32 EApiLibUnInitialize();

        public EapiInitializer()
        {
            _eApiStatusCodes = new EApiStatusCodes();
        }

        
        public void Init()
        {
            var result = EApiLibInitialize();
            if (!_eApiStatusCodes.IsSuccess(result))
            {
                throw new Exception("Eapi Init failed: " + _eApiStatusCodes.GetStatusStringFrom(result));
            }
        }

        public void Dispose()
        {
            var result = EApiLibUnInitialize();
            if (!_eApiStatusCodes.IsSuccess(result))
            {
                throw new Exception("Eapi Uninitialize failed: " + _eApiStatusCodes.GetStatusStringFrom(result));
            }
        }
    }
}
