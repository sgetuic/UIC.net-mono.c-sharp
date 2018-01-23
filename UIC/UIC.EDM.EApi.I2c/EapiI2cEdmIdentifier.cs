using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.EApi.I2c
{
    public class EapiI2cEdmIdentifier : EdmIdentifier
    {
        public Guid Id { get; }
        public string Uri { get; }

        public EapiI2cEdmIdentifier(string uri) {
            Uri = uri;
            Id = new Guid("{466153a9-b3e3-4d6f-8f66-c756a0101994}");
        }
    }
}