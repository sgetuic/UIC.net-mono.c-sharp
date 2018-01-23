using System;
using System.Runtime.InteropServices;

namespace UIC.EDM.EApi.Shared
{
    public class EapiInitializer
    {
        private readonly EApiStatusCodes _eApiStatusCodes;

        private static bool _initDone = false;
        private static bool _isDisposed = false;
        private static bool _initFailed = false;

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
            if (_initFailed) {
                throw new Exception("Eapi Init failed");
            }

            if (_initDone) return;
            _initDone = true;
            
            try
            {
                var result = EApiLibInitialize();
                if (!_eApiStatusCodes.IsSuccess(result))
                {
                    throw new Exception("Eapi Init failed: " + _eApiStatusCodes.GetStatusStringFrom(result));
                }
            }
            catch (Exception) {
                _initFailed = true;
                throw;
            }
        }

        public void Dispose()
        {

            if (_isDisposed) return;
            _isDisposed = true;

            var result = EApiLibUnInitialize();
            if (!_eApiStatusCodes.IsSuccess(result))
            {
                throw new Exception("Eapi Uninitialize failed: " + _eApiStatusCodes.GetStatusStringFrom(result));
            }
        }
    }
}
