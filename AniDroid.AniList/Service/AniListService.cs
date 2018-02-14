using System;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using System.Threading;
using AniDroid.AniList.Models;
using System.IO;
using Newtonsoft.Json;
using RestSharp.Serializers;
using Newtonsoft.Json.Serialization;
using AniDroid.AniList.Interfaces;
using RestSharp.Deserializers;
using AniDroid.AniList.Queries;
using Newtonsoft.Json.Linq;
using AniDroid.AniList.GraphQL;
using AniDroid.AniList.Utils;
using OneOf;

namespace AniDroid.AniList.Service
{
    public class AniListService : IAniListService
    {
        public IAniListServiceConfig Config { get; }
        public IAuthCodeResolver AuthCodeResolver { get; }

        public AniListService(IAniListServiceConfig config, IAuthCodeResolver auth)
        {
            Config = config;
            AuthCodeResolver = auth;
        }

        public static async Task<IRestResponse<AniListAuthorizationResponse>> AuthenticateUser(IAniListAuthConfig config, string code, CancellationToken cToken = default)
        {
            var authReq = new RestRequest(config.AuthUrl, Method.POST);
            authReq.AddParameter("client_id", config.ClientId);
            authReq.AddParameter("client_secret", config.ClientSecret);
            authReq.AddParameter("grant_type", "refresh_token");
            authReq.AddParameter("redirect_uri", config.RedirectUrl);
            authReq.AddParameter("code", code);

            var client = new RestClient();
            return await client.ExecuteTaskAsync<AniListAuthorizationResponse>(authReq, cToken).ConfigureAwait(false);
        }

        #region Media

        public async Task<OneOf<Media, IAniListError>> GetMedia(int id, Media.MediaType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaByIdAndType,
                Variables = JsonConvert.SerializeObject(new { id, type = type.Value })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<Media>(req, cToken);
        }


        public async Task<OneOf<AniListObject.PagedData<List<Media>>, IAniListError>> SearchMedia(string queryText, int page, int count, Media.MediaType type = null, CancellationToken cToken = default)
        {
            var variableObj = JObject.FromObject(new { queryText, page, count }, new JsonNetSerializer().Serializer);

            if (type != null)
            {
                variableObj.Add("type", type.Value);
            }

            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchMedia,
                Variables = variableObj.ToString()
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<Media>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<Media>>> SearchMediaPaging(string queryText,
            Media.MediaType type = null, int perPage = 20)
        {
            var arguments = new
            {
                queryText,
                type = type?.Value,
            };
            return new PagedAsyncEnumerable<ICollection<Media>>(perPage,
                CreateGetPageFunc<ICollection<Media>>(QueryStore.SearchMedia, arguments),
                HasNextPage);
        }

        #endregion

        #region User

        public async Task<OneOf<User, IAniListError>> GetUser(string name, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserByName,
                Variables = JsonConvert.SerializeObject(new { name })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<User>(req, cToken);
        }

        public async Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaListsByUserNameAndType,
                Variables = JsonConvert.SerializeObject(new { name = userName, type = type.Value })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<Media.MediaListCollection>(req, cToken);
        }

        public async Task<OneOf<AniListObject.PagedData<List<User>>, IAniListError>> SearchUsers(string queryText, int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchUsers,
                Variables = JsonConvert.SerializeObject(new { queryText, page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<User>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<User>>> SearchUsersPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ICollection<User>>(perPage,
                CreateGetPageFunc<ICollection<User>>(QueryStore.SearchUsers, arguments),
                HasNextPage);
        }

        #endregion

        #region Activity

        public async Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.ToggleLike,
                Variables = JsonConvert.SerializeObject(new { id, type = type.Value })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<List<User>>(req, cToken);
        }

        public async Task<OneOf<AniListObject.PagedData<List<AniListActivity>>, IAniListError>> GetAniListActivity(int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserActivity,
                Variables = JsonConvert.SerializeObject(new { page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<AniListActivity>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<AniListActivity>>> GetAniListActivityPaging(int perPage = 20)
        {
            return new PagedAsyncEnumerable<ICollection<AniListActivity>>(perPage,
                CreateGetPageFunc<ICollection<AniListActivity>>(QueryStore.GetUserActivity, null),
                HasNextPage);
        }

        public async Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.PostTextActivity,
                Variables = JsonConvert.SerializeObject(new { text })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListActivity>(req, cToken);
        }

        public async Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.PostActivityReply,
                Variables = JsonConvert.SerializeObject(new { activityId, text })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListActivity.ActivityReply>(req, cToken);
        }

        public async Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetAniListActivityById,
                Variables = JsonConvert.SerializeObject(new { id })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListActivity>(req, cToken);
        }

        public async Task<OneOf<AniListObject.PagedData<List<AniListNotification>>, IAniListError>> GetAniListNotifications(int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserNotifications,
                Variables = JsonConvert.SerializeObject(new { page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<AniListNotification>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<AniListNotification>>> GetAniListNotificationsPaging(int perPage = 20)
        {
            return new PagedAsyncEnumerable<ICollection<AniListNotification>>(perPage,
                CreateGetPageFunc<ICollection<AniListNotification>>(QueryStore.GetUserNotifications, null),
                HasNextPage);
        }

        #endregion

        #region Character

        public async Task<OneOf<AniListObject.PagedData<List<Character>>, IAniListError>> SearchCharacters(string queryText, int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchCharacters,
                Variables = JsonConvert.SerializeObject(new { queryText, page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<Character>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<Character>>> SearchCharactersPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ICollection<Character>>(perPage,
                CreateGetPageFunc<ICollection<Character>>(QueryStore.SearchCharacters, arguments),
                HasNextPage);
        }

        public async Task<OneOf<Character, IAniListError>> GetCharacterById(int id, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetCharacterById,
                Variables = JsonConvert.SerializeObject(new { id })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<Character>(req, cToken);
        }

        #endregion

        #region Staff

        public async Task<OneOf<AniListObject.PagedData<List<Staff>>, IAniListError>> SearchStaff(string queryText, int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchStaff,
                Variables = JsonConvert.SerializeObject(new { queryText, page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<Staff>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<Staff>>> SearchStaffPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ICollection<Staff>>(perPage,
                CreateGetPageFunc<ICollection<Staff>>(QueryStore.SearchStaff, arguments),
                HasNextPage);
        }

        #endregion

        #region Studio

        public async Task<OneOf<AniListObject.PagedData<List<Studio>>, IAniListError>> SearchStudios(string queryText, int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchStudios,
                Variables = JsonConvert.SerializeObject(new { queryText, page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<Studio>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<Studio>>> SearchStudiosPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ICollection<Studio>>(perPage,
                CreateGetPageFunc<ICollection<Studio>>(QueryStore.SearchStudios, arguments),
                HasNextPage);
        }

        #endregion

        #region ForumThread

        public async Task<OneOf<AniListObject.PagedData<List<ForumThread>>, IAniListError>> SearchForumThreads(string queryText, int page, int count, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SearchForumThreads,
                Variables = JsonConvert.SerializeObject(new { queryText, page, count })
            };
            var req = CreateRequest(query);
            return await GetResponseAsync<AniListObject.PagedData<List<ForumThread>>>(req, cToken);
        }

        public IAsyncEnumerable<AniListObject.PagedData<ICollection<ForumThread>>> SearchForumThreadsPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ICollection<ForumThread>>(perPage,
                CreateGetPageFunc<ICollection<ForumThread>>(QueryStore.SearchForumThreads, arguments),
                HasNextPage);
        }

        #endregion

        #region Internal

        /// <summary>
        /// Creates an IRestClient based off of the configuration and auth resolver passed into the service's constructor.
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Creates a new IRestRequest based on the provided GraphQL query.
        /// This sets the JSON serializer properly, and also sets the method as POST.
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        private static IRestRequest CreateRequest(GraphQLQuery query)
        {
            var req = new RestRequest(Method.POST)
            {
                JsonSerializer = JsonNetSerializer.Default
            };
            req.AddJsonBody(query);
            return req;
        }

        /// <summary>
        /// Asynchronously processes the provided IRestRequest, returning a discriminated union of the deserialized object (according to the type parameter), and an IAniListError.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="req"></param>
        /// <param name="cToken"></param>
        /// <returns></returns>
        private async Task<OneOf<T, IAniListError>> GetResponseAsync<T>(IRestRequest req, CancellationToken cToken) where T : class
        {
            var servResp = await CreateClient().ExecuteTaskAsync<GraphQLResponse<T>>(req, cToken).ConfigureAwait(false);

            if (servResp.IsSuccessful)
            {
                return servResp.Data.Value;
            }

            return new AniListError(servResp.ErrorMessage, servResp.ErrorException, servResp.Data?.Errors);
        }

        private Func<PagingInfo, CancellationToken, Task<OneOf<AniListObject.PagedData<T>, IAniListError>>> CreateGetPageFunc<T>(string query,
            object variables)
        {
            async Task<OneOf<AniListObject.PagedData<T>, IAniListError>> GetPageAsync(PagingInfo info, CancellationToken ct)
            {
                var vars = JObject.FromObject(variables ?? new object(), JsonNetSerializer.Default.Serializer);
                vars.Add("page", info.Page);
                vars.Add("count", info.PageSize);

                return await GetResponseAsync<AniListObject.PagedData<T>>(CreateRequest(new GraphQLQuery(query, vars)), ct);
            }

            return GetPageAsync;
        }

        private static bool HasNextPage<T>(PagingInfo info, AniListObject.PagedData<T> data) => data.PageInfo.HasNextPage;

        private interface IJsonSerializer : ISerializer, IDeserializer
        {
        }

        internal class JsonNetSerializer : IJsonSerializer
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

            public static JsonNetSerializer Default => new JsonNetSerializer();
        }

        #endregion
    }
}
