using System;
using System.Collections.Generic;
using System.Text;
using RestSharp;
using System.Threading.Tasks;
using System.Threading;
using AniDroid.AniList.Models;
using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;
using Newtonsoft.Json.Serialization;
using AniDroid.AniList.Authorization;
using AniDroid.AniList.Interfaces;
using RestSharp.Deserializers;
using AniDroid.AniList.Queries;

namespace AniDroid.AniList.Service
{
    public class AniListService : IAniListService
    {
        private IAniListServiceConfig Config { get; }
        private IAuthCodeResolver AuthCodeResolver { get; }

        private AniListService() { }
        
        public AniListService(IAniListServiceConfig config, IAuthCodeResolver auth)
        {
            Config = config;
            AuthCodeResolver = auth;
        }

        public static async Task<IRestResponse<AniListAuthorizationResponse>> AuthenticateUser(IAniListServiceConfig config, string code, CancellationToken cToken = default(CancellationToken))
        {
            var authReq = new RestRequest(config.AuthUrl, Method.POST);
            authReq.AddParameter("client_id", config.ClientId);
            authReq.AddParameter("client_secret", config.ClientSecret);
            authReq.AddParameter("grant_type", "refresh_token");
            authReq.AddParameter("redirect_uri", config.RedirectUrl);
            authReq.AddParameter("code", code);

            var client = new RestClient();
            return await client.ExecuteTaskAsync<AniListAuthorizationResponse>(authReq, cToken);
        }

        #region Media

        public async Task<IRestResponse<GraphQLResponse<Media>>> GetMedia(int id, Media.MediaType type, CancellationToken cToken = default(CancellationToken))
        {
            var client = CreateClient();
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaByIdAndType,
                Variables = JsonConvert.SerializeObject(new { id, type = type.Value })
            };
            var req = CreateRequest(Method.POST, query);
            return await client.ExecuteTaskAsync<GraphQLResponse<Media>>(req, cToken);
        }

        #endregion

        #region User

        public async Task<IRestResponse<GraphQLResponse<User>>> GetUser(string name, CancellationToken cToken = default(CancellationToken))
        {
            var client = CreateClient();
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserByName,
                Variables = JsonConvert.SerializeObject(new { name })
            };
            var req = CreateRequest(Method.POST, query);
            return await client.ExecuteTaskAsync<GraphQLResponse<User>>(req, cToken);
        }

        public async Task<IRestResponse<GraphQLResponse<Media.MediaListCollection>>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken = default(CancellationToken))
        {
            var client = CreateClient();
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaListsByUserNameAndType,
                Variables = JsonConvert.SerializeObject(new { name = userName, type = type.Value })
            };
            var req = CreateRequest(Method.POST, query);
            return await client.ExecuteTaskAsync<GraphQLResponse<Media.MediaListCollection>>(req, cToken);
        }

        public async Task<IRestResponse<GraphQLResponse<AniListObject.PagedData<List<UserActivity>>>>> GetUserActivity(CancellationToken cToken = default(CancellationToken))
        {
            var client = CreateClient();
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserActivity
            };
            var req = CreateRequest(Method.POST, query);
            return await client.ExecuteTaskAsync<GraphQLResponse<AniListObject.PagedData<List<UserActivity>>>>(req, cToken);
        }

        #endregion

        #region Internal

        private IRestClient CreateClient()
        {
            var client = new RestClient(Config.BaseUrl);
            client.ClearHandlers();
            client.AddHandler("*", JsonNetSerializer.Default);

            if (AuthCodeResolver.IsAuthorized)
            {
                client.AddDefaultHeader("Authorization", $"Bearer {AuthCodeResolver.AuthCode}");
            }

            return client;
        }

        private IRestRequest CreateRequest(Method method, GraphQLQuery query)
        {
            var req = new RestRequest(Method.POST)
            {
                JsonSerializer = JsonNetSerializer.Default
            };
            req.AddJsonBody(query);
            return req;
        }

        private interface IJsonSerializer : ISerializer, IDeserializer
        {
        }

        public class JsonNetSerializer : IJsonSerializer
        {
            public string DateFormat { get; set; }
            public string RootElement { get; set; }
            public string Namespace { get; set; }
            public string ContentType { get; set; }
            public Newtonsoft.Json.JsonSerializer Serializer { get; }

            public JsonNetSerializer()
            {
                Serializer = new Newtonsoft.Json.JsonSerializer
                {
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include
                };
                ContentType = "application/json";
            }

            public JsonNetSerializer(Newtonsoft.Json.JsonSerializer serializer)
            {
                Serializer = serializer;
                ContentType = "application/json";
            }

            public string Serialize(object obj)
            {
                using (var stringWriter = new StringWriter())
                {
                    using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                    {
                        jsonTextWriter.Formatting = Formatting.Indented;
                        jsonTextWriter.QuoteChar = '"';
                        Serializer.ContractResolver = new DefaultContractResolver { NamingStrategy = new CamelCaseNamingStrategy() };

                        Serializer.Serialize(jsonTextWriter, obj);

                        var result = stringWriter.ToString();
                        return result;
                    }
                }
            }

            public T Deserialize<T>(IRestResponse response)
            {
                var content = response.Content;

                using (var stringReader = new StringReader(content))
                {
                    using (var jsonTextReader = new JsonTextReader(stringReader))
                    {
                        return Serializer.Deserialize<T>(jsonTextReader);
                    }
                }
            }

            public static JsonNetSerializer Default
            {
                get
                {
                    return new JsonNetSerializer();
                }
            }
        }

        #endregion
    }
}
