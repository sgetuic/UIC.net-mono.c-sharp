using UIC.Util.Extensions;

namespace UIC.EDM.EApi.Shared
{
    public class EApiStatusCodes
    {
        public string GetStatusStringFrom(uint resultcode)
        {
            return resultcode.ToEnum<EAPI_STATUS_CODE>().ToString();
        }


        public enum EAPI_STATUS_CODE : uint
        {
            EAPI_STATUS_SUCCESS = 0,

            /* Description
             *   The EAPI library is not yet or unsuccessfully initialized. 
             *   EApiLibInitialize needs to be called prior to the first access of any 
             *   other EAPI function.
             * Actions
             *   Call EApiLibInitialize..
             */
            EAPI_STATUS_NOT_INITIALIZED = 0xFFFFFFFF,

            /* Description
             *   Library is initialized.
             * Actions
             *   none.
             */
            EAPI_STATUS_INITIALIZED = 0xFFFFFFFE,

            /* Description
             *   Memory Allocation Error.
             * Actions
             *   Free memory and try again..
             */
            EAPI_STATUS_ALLOC_ERROR = 0xFFFFFFFD,

            /* Description 
             *   Time out in driver. This is Normally caused by hardware/software 
             *   semaphore timeout. 
             * Actions
             *   Retry.
             */
            EAPI_STATUS_DRIVER_TIMEOUT = 0xFFFFFFFC,

            /* Description 
             *  One or more of the EAPI function call parameters are out of the 
             *  defined range.
             *
             *  Possible Reasons include be
             *  NULL Pointer
             *  Invalid Offset
             *  Invalid Length
             *  Undefined Value
             *  
             *   Storage Write
             *    Incorrectly Aligned Offset
             *    Invalid Write Length
             * Actions
             *   Verify Function Parameters.
             */
            EAPI_STATUS_INVALID_PARAMETER = 0xFFFFFEFF,

            /* Description
             *   The Block Alignment is incorrect.
             * Actions
             *   Use pInputs and pOutputs to correctly select input and outputs. 
             */
            EAPI_STATUS_INVALID_BLOCK_ALIGNMENT = 0xFFFFFEFE,

            /* Description
             *   This means that the Block length is too long.
             * Actions
             *   Use Alignment Capabilities information to correctly align write access.
             */
            EAPI_STATUS_INVALID_BLOCK_LENGTH = 0xFFFFFEFD,

            /* Description
             *   The current Direction Argument attempts to set GPIOs to a unsupported 
             *   directions. I.E. Setting GPI to Output.
             * Actions
             *   Use pInputs and pOutputs to correctly select input and outputs. 
             */
            EAPI_STATUS_INVALID_DIRECTION = 0xFFFFFEFC,

            /* Description
             *   The Bitmask Selects bits/GPIOs which are not supported for the current ID.
             * Actions
             *   Use pInputs and pOutputs to probe supported bits..
             */
            EAPI_STATUS_INVALID_BITMASK = 0xFFFFFEFB,

            /* Description
             *   Watchdog timer already started.
             * Actions
             *   Call EApiWDogStop, before retrying.
             */
            EAPI_STATUS_RUNNING = 0xFFFFFEFA,

            /* Description
             *   This function or ID is not supported at the actual hardware environment.
             * Actions
             *   none.
             */
            EAPI_STATUS_UNSUPPORTED = 0xFFFFFCFF,

            /* Description
             *   I2C Device Error
             *   No Acknowledge For Device Address, 7Bit Address Only
             *   10Bit Address may cause Write error if 2 10Bit addressed devices 
             *   present on the bus.
             * Actions
             *   none.
             */
            EAPI_STATUS_NOT_FOUND = 0xFFFFFBFF,

            /* Description
             *   I2C Time-out
             *   Device Clock stretching time-out, Clock pulled low by device 
             *   for too long
             * Actions
             *   none.
             */
            EAPI_STATUS_TIMEOUT = 0xFFFFFBFE,

            /* Description
             *   EApi  I2C functions specific. The addressed I2C bus is busy or there 
             *   is a bus collision.
             *   The I2C bus is in use. Either CLK or DAT are low.
             *   Arbitration loss or bus Collision, data remains low when writing a 1
             * Actions
             *   Retry.
             */
            EAPI_STATUS_BUSY_COLLISION = 0xFFFFFBFD,

            /* Description
             *   I2C Read Error
             *    Not Possible to detect.
             *   Storage Read Error
             *    ....
             * Actions
             *   Retry.
             */
            EAPI_STATUS_READ_ERROR = 0xFFFFFAFf,

            /* Description
             *   I2C Write Error
             *     No Acknowledge received after writing any Byte after the First Address 
             *     Byte.
             *     Can be caused by 
             *     unsupported Device Command/Index
             *     Ext Command/Index used on Standard Command/Index Device
             *     10Bit Address Device Not Present
             *   Storage Write Error
             *    ...
             * Actions
             *   Retry.
             */
            EAPI_STATUS_WRITE_ERROR = 0xFFFFFAFE,


            /* Description
             *   The amount of available data exceeds the buffer size.
             *   Storage buffer overflow was prevented. Read count was larger then 
             *   the defined buffer length.
             *   Read Count > Buffer Length
             * Actions
             *   Either increase the buffer size or reduce the block length.
             */
            EAPI_STATUS_MORE_DATA = 0xFFFFF9FF,

            /* Description
             *   Generic error message. No further error details are available.
             * Actions
             *   none.
             */
            EAPI_STATUS_ERROR = 0xFFFFF0FF,
        }

        public bool IsUnsupported(uint resultCode)
        {
            return resultCode == (uint)EAPI_STATUS_CODE.EAPI_STATUS_UNSUPPORTED;
        }

        public bool IsSuccess(uint resultCode)
        {
            return resultCode == (uint)EAPI_STATUS_CODE.EAPI_STATUS_SUCCESS;
        }
    }
}
