using System;

namespace UIC.Communication.M2mgo.ProjectAgent.WebApi.datamodel.Project
{
    public class SgetProjectPropertyInfo
    {
        public string Name { get; private set; }
        public string Key { get; private set; }
        public Guid HwInterfaceSchemeID { get; private set; }

        public SgetProjectPropertyInfo(string name, string key, Guid hwInterfaceSchemeID) {
            Name = name;
            Key = key;
            HwInterfaceSchemeID = hwInterfaceSchemeID;
        }

        public override string ToString()
        {
            return String.Format("{0} ({1} in Interface {2})", Name, Key, HwInterfaceSchemeID);
        }
    }
}