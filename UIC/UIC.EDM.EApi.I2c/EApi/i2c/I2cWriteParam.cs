namespace UIC.EDM.EApi.I2c.EApi.i2c
{
    internal class I2cWriteParam
    {
        public uint Addr { get; private set; }
        public uint Cmd { get; private set; }
        public byte[] Data { get; private set; }

        public I2cWriteParam(uint addr, uint cmd, byte[] data)
        {
            Addr = addr;
            Cmd = cmd;
            Data = data;
        }
    }
}