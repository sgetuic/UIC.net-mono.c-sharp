using System;

namespace UIC.EDM.EApi.I2c.EApi.i2c {
    internal class CommandMacros {
        internal uint EAPI_I2C_STD_CMD()
        {
            return (0<<30);
        }

        internal UInt32 EAPI_I2C_EXT_CMD()
        {
            int val = (2 << 30);
            return (uint) val;
        }

        internal uint EAPI_I2C_NO_CMD()
        {
            return (1 << 30);
        }

        internal uint EAPI_I2C_CMD_TYPE_MASK()
        {
            int val = (3 << 30);
            return (uint) val;
        }

        internal uint EAPI_I2C_ENC_STD_CMD(uint x)
        {
            return (((x) & 0xFF) | EAPI_I2C_STD_CMD());

        }

        internal uint EAPI_I2C_ENC_EXT_CMD(uint x)
        {
            return (((x) & 0xFFFF) | EAPI_I2C_EXT_CMD());
        }

        internal bool EAPI_I2C_IS_EXT_CMD(uint x)
        {
            return (((x) & (EAPI_I2C_CMD_TYPE_MASK())) == EAPI_I2C_EXT_CMD());
        }

        internal bool EAPI_I2C_IS_STD_CMD(uint x)
        {
            return (((x) & (EAPI_I2C_CMD_TYPE_MASK())) == EAPI_I2C_STD_CMD());
        }

        internal bool EAPI_I2C_IS_NO_CMD(uint x)
        {
            return (((x) & (EAPI_I2C_CMD_TYPE_MASK())) == EAPI_I2C_NO_CMD());
        }
    }
}
