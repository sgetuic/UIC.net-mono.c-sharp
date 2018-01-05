using System.IO;
using UIC.Util.Serialization;

namespace UIC.Communication.M2mgo.CommunicationAgent.WebApi
{
    internal class M2mgoUserTokenCache
    {
        private const string Path = @".\authtoken.json";

        public void Cache(string result)
        {
            if (File.Exists(Path))
            {
                File.Delete(Path);
            }
            File.WriteAllText(Path, result);
        }

        public M2mgoUserToken Get(ISerializer serializer)
        {
            if (File.Exists(Path))
            {
                return serializer.Deserialize<M2mgoUserToken>(File.ReadAllText(Path));
            }
            return null;
        }
    }
}