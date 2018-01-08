using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel
{
    public class CloudMapperIdentifier
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }

        public CloudMapperIdentifier(Guid id, string name)
        {
            if (id == null) throw new ArgumentNullException("id");
            Id = id;
            Name = name ?? String.Empty;
        }

        public override string ToString() {
            return String.Format("{0} ({1})", Name, Id);
        }
    }
}