using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.Test.Mockup
{
    public class MockupEdmIdentifier : EdmIdentifier
    {
        public Guid Id { get; }
        public string Uri { get; }

        public MockupEdmIdentifier(string uri) {
            Uri = uri;
            Id = new Guid("{1e7b861c-d6a0-4993-812e-0ffd42a4c77d}");
        }
    }
}