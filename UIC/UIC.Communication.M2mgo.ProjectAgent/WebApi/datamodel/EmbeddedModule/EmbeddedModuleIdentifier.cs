using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.EmbeddedModule
{
    public class EmbeddedModuleIdentifier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public EmbeddedModuleIdentifier(Guid id, string name)
        {
            Id = id;
            Name = name;
        }


        public override string ToString()
        {
            return String.Format("{0} ({1})", Name, Id.ToString("D"));
        }
    }
}