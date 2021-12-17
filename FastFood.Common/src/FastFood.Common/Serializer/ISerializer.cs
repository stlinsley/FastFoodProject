namespace FastFood.Common.Serializer
{
    public interface ISerializer
    {
        string Serialize<T>(T objectToSerialize);

        T Deserialize<T>(string serializedObject);
    }
}
