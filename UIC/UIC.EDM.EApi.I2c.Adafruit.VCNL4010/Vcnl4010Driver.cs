using System.Threading;
using UIC.EDM.EApi.I2c.EApi.i2c;
using UIC.Util.Logging;

namespace UIC.EDM.EApi.I2c.Adafruit.VCNL4010
{
    internal class Vcnl4010Driver
    {
        private readonly I2cDriver _driver;
        private readonly ILogger _logger;

        uint VCNL4010_I2CADDR_DEFAULT = 0x26;

        // commands and constants
        uint VCNL4010_COMMAND = 0x80;
        uint VCNL4010_PRODUCTID = 0x81;
        uint VCNL4010_PROXRATE = 0x82;
        uint VCNL4010_IRLED = 0x83;
        uint VCNL4010_AMBIENTPARAMETER = 0x84;
        uint VCNL4010_AMBIENTDATA = 0x85;
        uint VCNL4010_PROXIMITYDATA = 0x87;
        uint VCNL4010_INTCONTROL = 0x89;
        uint VCNL4010_PROXINITYADJUST = 0x8A;
        uint VCNL4010_INTSTAT = 0x8E;
        uint VCNL4010_MODTIMING = 0x8F;

        //enum
        // {
        //   VCNL4010_3M125   = 3,
        //   VCNL4010_1M5625  = 2,
        //   VCNL4010_781K25  = 1,
        //   VCNL4010_390K625 = 0,
        // } vcnl4010_freq;

        byte VCNL4010_MEASUREAMBIENT = 0x10;
        byte VCNL4010_MEASUREPROXIMITY = 0x08;
        uint VCNL4010_AMBIENTREADY = 0x40;
        uint VCNL4010_PROXIMITYREADY = 0x20;

        public Vcnl4010Driver(I2cDriver driver, ILogger logger)
        {
            _driver = driver;
            _logger = logger;
        }

        internal int Adafruit_VCNL4010_ReadAmbient()
        {
            // the i2c address

            lock (_driver)
            {
                uint readByte = _driver.ReadByte(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_INTSTAT));
                byte byteToWrite = (byte)(0x000000ff & (readByte & (~0x40)));

                _driver.Write(new I2cWriteParam(VCNL4010_I2CADDR_DEFAULT, VCNL4010_INTSTAT, new[] { byteToWrite }));
                _driver.Write(new I2cWriteParam(VCNL4010_I2CADDR_DEFAULT, VCNL4010_COMMAND, new[] { VCNL4010_MEASUREAMBIENT }));

                while (true)
                {
                    var result = _driver.ReadByte(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_COMMAND));
                    if ((result & VCNL4010_AMBIENTREADY) > 1)
                    {
                        byte[] bytes = _driver.Read(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_AMBIENTDATA), 2);
                        int data = (bytes[0] << 8) | bytes[1];
                        _logger.Information("Adafruit_VCNL4010_ReadAmbient: " + data);
                        return data;
                    }

                    Thread.Sleep(10);
                }
            }
            
        }

        internal int Adafruit_VCNL4010_ReadProximity()
        {
            lock (_driver)
            {

                uint readByte = _driver.ReadByte(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_INTSTAT));
                byte byteToWrite = (byte)(0x000000ff & (readByte & (~0x80)));

                _driver.Write(new I2cWriteParam(VCNL4010_I2CADDR_DEFAULT, VCNL4010_INTSTAT, new[] { byteToWrite }));
                _driver.Write(new I2cWriteParam(VCNL4010_I2CADDR_DEFAULT, VCNL4010_COMMAND, new[] { VCNL4010_MEASUREPROXIMITY }));

                while (true)
                {
                    var result = _driver.ReadByte(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_COMMAND));
                    if ((result & VCNL4010_PROXIMITYREADY) > 1)
                    {
                        byte[] bytes = _driver.Read(new I2cReadParameter(VCNL4010_I2CADDR_DEFAULT, VCNL4010_PROXIMITYDATA),
                            2);
                        int data = (bytes[0] << 8) | bytes[1];
                        _logger.Information("Adafruit_VCNL4010_ReadProximity: " + data);
                        return data;
                    }

                    Thread.Sleep(10);
                }
            }
        
    }
    }
}
