namespace UIC.EDM.EApi.Gpio.Eapi
{
    public class GpioLevel
    {
        private readonly uint _levelArray;
        internal GpioLevel(uint levelArray)
        {
            _levelArray = levelArray;
        }

        public bool GetLevelOf(EapiGpioId pin)
        {
            return (_levelArray & (1 << (int)pin)) > 0;
        }
    }
}