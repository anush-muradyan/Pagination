using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace Convertor {
    public class JsonConvertor : IConvertor
    {
        private readonly JsonSerializerSettings settings;

        public JsonConvertor()
        {
            settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            settings.Converters.Add(new StringEnumConverter());
            settings.Converters.Add(new IsoDateTimeConverter());
        }

        public string Serialize(object data)
        {
            return JsonConvert.SerializeObject(data, Formatting.None, settings);
        }

        public T Deserialize<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data, settings);
        }
    }
}