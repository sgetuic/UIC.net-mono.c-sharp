using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.EApi.Gpio
{
    internal class GpioEdmIdentifier : EdmIdentifier
    {
        public GpioEdmIdentifier() {
            Id = new Guid("{d958bb24-7a1a-4cb6-bb7f-3a5868d3170d}");
            Uri = GetType().FullName;
        }
        public Guid Id { get; }
        public string Uri { get; }
    }
}