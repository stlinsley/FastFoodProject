namespace FastFood.Common.Serializer
{
    using Newtonsoft.Json;

    public class Serializer : ISerializer
    {
        public string Serialize<T>(T objectToSerialize) => JsonConvert.SerializeObject(objectToSerialize);

        public T Deserialize<T>(string serializedObject)
        {
            return !string.IsNullOrEmpty(serializedObject)
                ? JsonConvert.DeserializeObject<T>(serializedObject)
                : throw new System.ArgumentNullException($"'{nameof(serializedObject)}' cannot be null or empty.", nameof(serializedObject));        }
    }
}
