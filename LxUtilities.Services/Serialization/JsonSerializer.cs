using LxUtilities.Definitions.Serialization;
using Newtonsoft.Json;

namespace LxUtilities.Services.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object anyObject)
        {
            var jsonString = JsonConvert.SerializeObject(anyObject);
            return jsonString;
        }

        public T Deserialize<T>(string serialized)
        {
            var objectFromJson = JsonConvert.DeserializeObject<T>(serialized);
            return objectFromJson;
        }
    }
}