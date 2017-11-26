using System;
using UIC.Framework.Interfaces.Edm;

namespace UIC.EDM.EApi.BoardInformation
{
    public class EapiBoardInformationEdmIdentifier : Edmldentifier
    {
        public Guid Id { get; }
        public string Name { get; }

        public EapiBoardInformationEdmIdentifier() {
            Id = new Guid("{b3bb9ffa-43ba-49f9-bbb2-40d1de291215}");
            Name = "UIC.EDM.EApi.BoardInformation.Eapi.SystemInformationEdm";
        }
    }
}