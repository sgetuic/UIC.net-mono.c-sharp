namespace UIC.EDM.EApi.I2c.EApi.i2c
{
    internal class I2cReadParameter
    {
        public uint Addr { get; private set; }
        public uint Cmd { get; private set; }
        
        public I2cReadParameter(uint addr, uint cmd)
        {
            Addr = addr;
            Cmd = cmd;
        }
    }
}