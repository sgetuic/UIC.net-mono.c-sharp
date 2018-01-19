namespace UIC.EDM.EApi.I2c.EApi.i2c
{
    public class I2CBusCapability
    {
        public uint MaxBlockLen { get; private set; }
        public bool IsUnsupported { get; private set; }

        internal I2CBusCapability(uint maxBlockLen, bool isUnsupported = false)
        {
            MaxBlockLen = maxBlockLen;
            IsUnsupported = isUnsupported;
        }

        public override string ToString()
        {
            return string.Format("I2C Capabilities - {0} {1}", MaxBlockLen, (IsUnsupported ? " - [UNSUPPORTED]" : string.Empty));
        }
    }
}