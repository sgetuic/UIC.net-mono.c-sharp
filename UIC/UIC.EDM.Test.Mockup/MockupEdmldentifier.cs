using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.Test.Mockup
{
    public class MockupEdmldentifier : Edmldentifier
    {
        public Guid Id { get; }
        public string Uri { get; }

        public MockupEdmldentifier(string uri) {
            Uri = uri;
            Id = new Guid("{1e7b861c-d6a0-4993-812e-0ffd42a4c77d}");
        }
    }
}