using System.IO;
using AniDroid.AniList.Utils.Internal;
using Newtonsoft.Json;
using RestSharp;
using RestSharp.Deserializers;
using RestSharp.Serializers;

namespace AniDroid.AniList.Utils
{
    public class AniListJsonSerializer : ISerializer, IDeserializer
    {
        public string DateFormat { get; set; }
        public string RootElement { get; set; }
        public string Namespace { get; set; }
        public string ContentType { get; set; }
        public Newtonsoft.Json.JsonSerializer Serializer { get; }

        public AniListJsonSerializer()
        {
            Serializer = new Newtonsoft.Json.JsonSerializer
            {
                MissingMemberHandling = MissingMemberHandling.Ignore,
                NullValueHandling = NullValueHandling.Ignore,
                DefaultValueHandling = DefaultValueHandling.Include,
                ContractResolver = AniListContractResolver.Instance,
            };
            ContentType = "application/json";
        }

        public AniListJsonSerializer(Newtonsoft.Json.JsonSerializer serializer)
        {
            Serializer = serializer;
            ContentType = "application/json";
        }

        public string Serialize(object obj)
        {
            using (var stringWriter = new StringWriter())
            using (var jsonTextWriter = new JsonTextWriter(stringWriter))
            {
                Serializer.Serialize(jsonTextWriter, obj);
                return stringWriter.ToString();
            }
        }

        public T Deserialize<T>(IRestResponse response)
        {
            var content = response.Content;

            using (var stringReader = new StringReader(content))
            using (var jsonTextReader = new JsonTextReader(stringReader))
            {
                return Serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public T Deserialize<T>(string content)
        {
            using (var stringReader = new StringReader(content))
            using (var jsonTextReader = new JsonTextReader(stringReader))
            {
                return Serializer.Deserialize<T>(jsonTextReader);
            }
        }

        public static AniListJsonSerializer Default => new AniListJsonSerializer();
    }
}
