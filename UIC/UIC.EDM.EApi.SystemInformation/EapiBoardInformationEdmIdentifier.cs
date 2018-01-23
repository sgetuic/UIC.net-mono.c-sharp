using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmIdentifier : EdmIdentifier
    {
        public Guid Id { get; }
        public string Uri { get; }

        public EapiBoardInformationEdmIdentifier(string uri) {
            Uri = uri;
            Id = new Guid("{b3bb9ffa-43ba-49f9-bbb2-40d1de291215}");
        }
    }
}