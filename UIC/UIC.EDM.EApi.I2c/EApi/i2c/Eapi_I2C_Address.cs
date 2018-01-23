namespace UIC.EDM.EApi.I2c.EApi.i2c {
    internal class Eapi_I2C_Address {
        private readonly uint _addr;

        public Eapi_I2C_Address(uint addr)
        {
            _addr = addr;
        }

        public uint GetEncoded7BitAddress()
        {
            return EAPI_I2C_ENC_7BIT_ADDR(_addr);
        }

        private uint EAPI_I2C_ENC_7BIT_ADDR(uint x)
        {
            return ((x) & 0x07F) << 1;
        }

        private uint EAPI_I2C_DEC_7BIT_ADDR(uint x)
        {
            return (((x) >> 1) & 0x07F);
        }

        private uint EAPI_I2C_ENC_10BIT_ADDR(uint x)
        {
            return (((x) & 0xFF) | (((x) & 0x0300) << 1) | 0xF000);
        }

        private uint EAPI_I2C_DEC_10BIT_ADDR(uint x)
        {
            return (((x) & 0xFF) | (((x) >> 1) & 0x300));
        }

        private bool EAPI_I2C_IS_10BIT_ADDR(uint x)
        {
            return (((x) & 0xF800) == 0xF000);
        }

        private bool EAPI_I2C_IS_7BIT_ADDR(uint x)
        {
            return (!EAPI_I2C_IS_10BIT_ADDR(x));
        }

    }
}
