namespace UIC.Util.Serialization
{
    public interface ISerializer
    {
        string Serialize(object value, bool formatted = false);
        T Deserialize<T>(string value);
    }
}