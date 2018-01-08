using Newtonsoft.Json;

namespace UIC.Util.Serialization
{
    public class UicSerializer : ISerializer
    {
        public string Serialize(object value, bool formatted = false) {
            if (formatted) {
                return JsonConvert.SerializeObject(value, Formatting.Indented);
            }

            return JsonConvert.SerializeObject(value);
        }
        
        public T Deserialize<T>(string value) {
            return JsonConvert.DeserializeObject<T>(value);
        }
    }
}