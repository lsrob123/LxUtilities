using LxUtilities.Definitions.Serialization;
using Newtonsoft.Json;

namespace LxUtilities.Services.Serialization
{
    public class JsonSerializer : ISerializer
    {
        public string Serialize(object anyObject, bool useFullContractResolver = false)
        {
            if (!useFullContractResolver)
            {
                var s1 = JsonConvert.SerializeObject(anyObject);
                return s1;
            }

            var setting = new JsonSerializerSettings {ContractResolver = new FullJsonContractResolver()};
            var s2 = JsonConvert.SerializeObject(anyObject, setting);
            return s2;
        }

        public T Deserialize<T>(string serialized, bool useFullContractResolver = true)
        {
            if (!useFullContractResolver)
            {
                var o1 = JsonConvert.DeserializeObject<T>(serialized);
                return o1;
            }

            var setting = new JsonSerializerSettings {ContractResolver = new FullJsonContractResolver()};
            var o2 = JsonConvert.DeserializeObject<T>(serialized, setting);
            return o2;
        }
    }
}