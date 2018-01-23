using System;
using System.Runtime.InteropServices;
using System.Text;
using UIC.EDM.EApi.Shared;

namespace UIC.EDM.EApi.BoardInformation.EApi.BoardInformation {
    internal class BoardInformationDriver
    {
        private readonly EApiStatusCodes _eApiStatusCodes;
        
        public BoardInformationDriver()
        {
            _eApiStatusCodes = new EApiStatusCodes();
        }

// ReSharper disable once InconsistentNaming
        private const uint EAPI_KELVINS_OFFSET = 2731;

        private uint EAPI_ENCODE_CELCIUS(uint celsius)
        {
            return (((celsius)*10)) + EAPI_KELVINS_OFFSET;
        }

        private uint EAPI_DECODE_CELCIUS(uint celsius)
        {
            return ((celsius) - EAPI_KELVINS_OFFSET)/10;
        }

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiBoardGetValue(UInt32 id, ref UInt32 value);

        [DllImport("Eapi_1.dll")]
        private static extern UInt32 EApiBoardGetStringA(UInt32 id, StringBuilder pBuffer, ref UInt32 pBufferLength);


        internal uint GetBoardInformationOf(BoardInformationValueId boardInformationValueId) {
            uint value = 0;
            uint resultCode = EApiBoardGetValue((uint)boardInformationValueId, ref value);

            if (_eApiStatusCodes.IsUnsupported(resultCode))
            {
                throw new NotSupportedException(boardInformationValueId.ToString());
            }

            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception(_eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
            
            if (boardInformationValueId == BoardInformationValueId.EAPI_ID_HWMON_CHIPSET_TEMP
                || boardInformationValueId == BoardInformationValueId.EAPI_ID_HWMON_CPU_TEMP
                || boardInformationValueId == BoardInformationValueId.EAPI_ID_HWMON_SYSTEM_TEMP)
            {
                return EAPI_DECODE_CELCIUS(value);
            }
            return value;
        }

        internal string GetBoardInformationOf(BoardInformationStringId boardInformationStringId) {
            uint bufferLength = 5000;
            var buffer = new StringBuilder((int)bufferLength);

            uint resultCode = EApiBoardGetStringA((uint)boardInformationStringId, buffer, ref bufferLength);
            if (_eApiStatusCodes.IsUnsupported(resultCode))
            {
                throw new NotSupportedException(boardInformationStringId.ToString());
            }
            if (!_eApiStatusCodes.IsSuccess(resultCode)) {
                throw new Exception(_eApiStatusCodes.GetStatusStringFrom(resultCode));
            }
            return buffer.ToString();
        }
    }
}
