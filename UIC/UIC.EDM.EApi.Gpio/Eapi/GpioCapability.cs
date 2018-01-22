using System;
using System.Collections.Generic;

namespace UIC.EDM.EApi.Gpio.Eapi
{
    public class GpioCapability
    {
        private Dictionary<EapiGpioId, bool> pinIsOutputMap = new Dictionary<EapiGpioId, bool>();
        public uint InputCapabilityMask { get; private set; }
        public uint OutputCapabilityMask { get; private set; }
        public bool IsUnsupported { get; private set; }

        internal GpioCapability(uint inputCapabilityMask, uint outputCapabilityMask, bool isUnsupported = false)
        {
            IsUnsupported = isUnsupported;
            InputCapabilityMask = inputCapabilityMask;
            OutputCapabilityMask = outputCapabilityMask;
            for (int i = 0; i < 16; i++)
            {
                pinIsOutputMap.Add((EapiGpioId)i, (OutputCapabilityMask & (1 << i)) > 0);

            }
        }

        public override string ToString()
        {
            return string.Format("GPIO Capabilities - Inputs: {0} - Outputs: {1} {2}", 
                Convert.ToString(InputCapabilityMask, 2).PadLeft(16, '0'), 
                Convert.ToString(OutputCapabilityMask, 2).PadLeft(16, '0'), 
                (IsUnsupported ? " - [UNSUPPORTED]" : string.Empty));
        }

        public bool IsOutput(EapiGpioId id)
        {
            return pinIsOutputMap[id];
        }
    }
}