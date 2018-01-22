using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.EApi.I2c.Adafruit.VCNL4010
{
    internal class Vcnl4010Identifier : EdmIdentifier
    {
        public Vcnl4010Identifier() {
            Id = new Guid("{7a7f387b-554d-411c-b91d-ed3b9a3361b2}");
            Uri = GetType().FullName;
        }
        public Guid Id { get; }
        public string Uri { get; }
    }
}