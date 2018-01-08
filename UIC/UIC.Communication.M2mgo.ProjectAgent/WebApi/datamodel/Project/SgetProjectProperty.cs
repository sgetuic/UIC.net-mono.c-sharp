using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project
{
    public class SgetProjectProperty
    {
        public SgetProjectProperty(Guid attributeID, SgetProjectPropertyInfo info) {
            AttributeID = attributeID;
            Info = info;
        }

        public Guid AttributeID { get; private set; }
        public SgetProjectPropertyInfo Info { get; private set; }

        public override string ToString()
        {
            return Info.ToString();
        }
    }
}