namespace Application.Components.Serializer
{
    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public class JsonStringSerializer : IStringSerializer
    {
        private readonly JsonSerializerSettings settings;

        public JsonStringSerializer()
        {
            settings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateTimeZoneHandling = DateTimeZoneHandling.Utc,
                DateFormatHandling = DateFormatHandling.IsoDateFormat
            };
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.None, settings);
        }

        public T Deserialize<T>(string str)
        {
            return JsonConvert.DeserializeObject<T>(str, settings);
        }
    }
}
