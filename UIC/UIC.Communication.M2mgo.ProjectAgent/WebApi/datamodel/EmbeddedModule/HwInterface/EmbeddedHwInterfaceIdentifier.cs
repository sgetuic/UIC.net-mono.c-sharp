using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule.HwInterface
{
    public class EmbeddedHwInterfaceIdentifier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public EmbeddedHwInterfaceIdentifier(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public override string ToString()
        {
            return String.Format("Interface: {0} ({1})", Name, Id.ToString("D"));
        }
    }
}