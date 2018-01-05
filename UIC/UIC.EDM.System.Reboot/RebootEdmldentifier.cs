using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.System.Reboot
{
    public class RebootEdmldentifier : Edmldentifier
    {
        public Guid Id { get; }
        public string Uri { get; }

        public RebootEdmldentifier(string uri) {
            Uri = uri;
            Id = new Guid("{097c8855-d34e-4aff-9603-cb1f9a766a20}");
        }
    }
}