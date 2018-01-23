using System;
using System.IO;
using System.Runtime.InteropServices;
using UIC.EDM.EApi.Shared;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.I2c.EApi.i2c {
    internal class I2cDriver {
        private readonly ILogger _logger;
        private readonly EApiStatusCodes _eApiStatusCodes;

        public I2cDriver(ILogger logger, EApiStatusCodes eApiStatusCodes)
        {
            _logger = logger;
            _eApiStatusCodes = eApiStatusCodes;
        }

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiI2CGetBusCap(UInt32 id, ref UInt32 maxBlkLen);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiI2CWriteTransfer(UInt32 i2CBusId, UInt32 addr, UInt32 cmd, byte[] buffer, UInt32 byteCnt);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiI2CReadTransfer(UInt32 i2CBusId, UInt32 addr, UInt32 cmd, byte[] buffer, UInt32 bufLen, UInt32 bytgeCnt);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiI2CProbeDevice(UInt32 i2CBusId, UInt32 addr);

    
        internal I2CBusCapability GetBusCapabilities()
        {
            uint maxBlockLen = 0;
            uint resultCode = EApiI2CGetBusCap((uint) Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL, ref maxBlockLen);

            if (_eApiStatusCodes.IsUnsupported(resultCode))
            {
                _logger.Warning("EApiI2CGetBusCap " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + " is unsupported");
                return new I2CBusCapability(maxBlockLen, true);
            }
            if (!_eApiStatusCodes.IsSuccess(resultCode))
            {
                throw new Exception("EApiI2CGetBusCap " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + ": " +
                                    _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
            return new I2CBusCapability(maxBlockLen);
        }

        

        

        public byte[] Read(I2cReadParameter param, uint byteCount) {
            I2CBusCapability capability = GetBusCapabilities();
            if (capability.IsUnsupported) {
                throw new Exception("I2C Bus not supported");
            }

            if (byteCount > capability.MaxBlockLen) {
                throw new IOException("Byte count of " + byteCount + " exceeds max Block Length of " +
                                      capability.MaxBlockLen);
            }
            
            var buf = new byte[byteCount];

            uint resultCode = EApiI2CReadTransfer((uint)Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL, param.Addr, param.Cmd, buf, (uint)buf.Length, byteCount);
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception("EApiI2CReadTransfer " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + ": " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
            //_logger.Information("Read Bytes: " + BitConverter.ToString(buf));
            return buf;
        }

        internal byte ReadByte(I2cReadParameter param) {
            byte[] buf = Read(param, 1);
            return buf[0];
        }

        internal void Write(I2cWriteParam param) {
            I2CBusCapability capability = GetBusCapabilities();
            if (capability.IsUnsupported) {
                throw new Exception("I2C Bus not supported");
            }

            if (capability.MaxBlockLen < param.Data.Length) {
                throw new IOException("Byte count of " + param.Data.Length + " exceeds max Block Length of " + capability.MaxBlockLen);
            }

            uint resultCode = EApiI2CWriteTransfer((uint)Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL, param.Addr, param.Cmd, param.Data, (uint)param.Data.Length);
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception("EApiI2CWriteTransfer " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + ": " + _eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
        }

        internal bool ProbeDevice(Eapi_I2C_Address address) {
            uint resultCode = EApiI2CProbeDevice((uint)Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL, address.GetEncoded7BitAddress());

            if (_eApiStatusCodes.IsUnsupported(resultCode)) {
                _logger.Warning("ProbeDevice " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + " is unsupported");
                return false;
            }
            if (_eApiStatusCodes.IsSuccess(resultCode)) {
                return true;
            }
            _logger.Error("ProbeDevice " + Eapi_I2C_ID.EAPI_ID_I2C_EXTERNAL + ": " +
                                    _eApiStatusCodes.GetStatusStringFrom(resultCode));
            return false;
        }
    }
}
