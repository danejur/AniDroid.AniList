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
using AniDroid.AniList.Utils.Internal;
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

        public static Task<IRestResponse<AniListAuthorizationResponse>> AuthenticateUser(IAniListAuthConfig config, string code, CancellationToken cToken = default)
        {
            var authReq = new RestRequest(config.AuthUrl, Method.POST);
            authReq.AddParameter("client_id", config.ClientId);
            authReq.AddParameter("client_secret", config.ClientSecret);
            authReq.AddParameter("grant_type", "refresh_token");
            authReq.AddParameter("redirect_uri", config.RedirectUrl);
            authReq.AddParameter("code", code);

            var client = new RestClient();
            return client.ExecuteTaskAsync<AniListAuthorizationResponse>(authReq, cToken);
        }

        #region Media

        public Task<OneOf<Media, IAniListError>> GetMedia(int id, Media.MediaType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaByIdAndType,
                Variables = new { id, type = type.Value },
            };
            return GetResponseAsync<Media>(query, cToken);
        }

        public IAsyncEnumerable<IPagedData<Media>> SearchMediaPaging(string queryText,
            Media.MediaType type = null, int perPage = 20)
        {
            var arguments = new
            {
                queryText,
                type = type?.Value,
            };
            return new PagedAsyncEnumerable<Media>(perPage,
                CreateGetPageFunc<Media>(QueryStore.SearchMedia, arguments),
                HasNextPage);
        }

        #endregion

        #region User

        public Task<OneOf<User, IAniListError>> GetUser(string name, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserByName,
                Variables = new { name },
            };
            return GetResponseAsync<User>(query, cToken);
        }

        public Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaListsByUserNameAndType,
                Variables = new { name = userName, type = type.Value },
            };
            return GetResponseAsync<Media.MediaListCollection>(query, cToken);
        }

        public IAsyncEnumerable<IPagedData<User>> SearchUsersPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<User>(perPage,
                CreateGetPageFunc<User>(QueryStore.SearchUsers, arguments),
                HasNextPage);
        }

        #endregion

        #region Activity

        public Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.ToggleLike,
                Variables = new { id, type = type.Value },
            };
            return GetResponseAsync<List<User>>(query, cToken);
        }

        public IAsyncEnumerable<IPagedData<AniListActivity>> GetAniListActivityPaging(int perPage = 20)
        {
            return new PagedAsyncEnumerable<AniListActivity>(perPage,
                CreateGetPageFunc<AniListActivity>(QueryStore.GetUserActivity, null),
                HasNextPage);
        }

        public Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.PostTextActivity,
                Variables = new { text },
            };
            return GetResponseAsync<AniListActivity>(query, cToken);
        }

        public Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.PostActivityReply,
                Variables = new { activityId, text },
            };
            return GetResponseAsync<AniListActivity.ActivityReply>(query, cToken);
        }

        public Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetAniListActivityById,
                Variables = new { id },
            };
            return GetResponseAsync<AniListActivity>(query, cToken);
        }

        public IAsyncEnumerable<IPagedData<AniListNotification>> GetAniListNotificationsPaging(int perPage = 20)
        {
            return new PagedAsyncEnumerable<AniListNotification>(perPage,
                CreateGetPageFunc<AniListNotification>(QueryStore.GetUserNotifications, null),
                HasNextPage);
        }

        #endregion

        #region Character

        public IAsyncEnumerable<IPagedData<Character>> SearchCharactersPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Character>(perPage,
                CreateGetPageFunc<Character>(QueryStore.SearchCharacters, arguments),
                HasNextPage);
        }

        public Task<OneOf<Character, IAniListError>> GetCharacterById(int id, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetCharacterById,
                Variables = new { id },
            };
            return GetResponseAsync<Character>(query, cToken);
        }

        #endregion

        #region Staff

        public IAsyncEnumerable<IPagedData<Staff>> SearchStaffPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Staff>(perPage,
                CreateGetPageFunc<Staff>(QueryStore.SearchStaff, arguments),
                HasNextPage);
        }

        public Task<OneOf<Staff, IAniListError>> GetStaffById(int id, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetStaffById,
                Variables = new { id },
            };
            return GetResponseAsync<Staff>(query, cToken);
        }

        public IAsyncEnumerable<IPagedData<Character.Edge>> GetStaffCharactersPaging(int staffId,
            int perPage)
        {
            var arguments = new { staffId };
            return new PagedAsyncEnumerable<Character.Edge>(perPage,
                CreateGetPageFunc<Character.Edge, Staff>(QueryStore.GetStaffCharacters, arguments, (staff) => staff.Characters),
                HasNextPage);
        }

        #endregion

        #region Studio

        public IAsyncEnumerable<IPagedData<Studio>> SearchStudiosPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Studio>(perPage,
                CreateGetPageFunc<Studio>(QueryStore.SearchStudios, arguments),
                HasNextPage);
        }

        #endregion

        #region ForumThread

        public IAsyncEnumerable<IPagedData<ForumThread>> SearchForumThreadsPaging(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ForumThread>(perPage,
                CreateGetPageFunc<ForumThread>(QueryStore.SearchForumThreads, arguments),
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

        private async Task<OneOf<T, IAniListError>> GetResponseAsync<TResponse, T>(IRestRequest req, Func<TResponse, T> getCollection, CancellationToken cToken) where T : class where TResponse : class
        {
            var servResp = await CreateClient().ExecuteTaskAsync<GraphQLResponse<TResponse>>(req, cToken).ConfigureAwait(false);

            if (servResp.IsSuccessful)
            {
                return getCollection(servResp.Data.Value);
            }

            return new AniListError(servResp.ErrorMessage, servResp.ErrorException, servResp.Data?.Errors);
        }

        private Task<OneOf<T, IAniListError>> GetResponseAsync<T>(GraphQLQuery query, CancellationToken cToken)
            where T : class
        {
            return GetResponseAsync<T>(CreateRequest(query), cToken);
        }

        private Func<PagingInfo, CancellationToken, Task<OneOf<IPagedData<T>, IAniListError>>> CreateGetPageFunc<T>(string queryString,
            object variables)
        {
            Task<OneOf<IPagedData<T>, IAniListError>> GetPageAsync(PagingInfo info, CancellationToken ct)
            {
                var vars = JObject.FromObject(variables ?? new object(), JsonNetSerializer.Default.Serializer);
                vars.Add("page", info.Page);
                vars.Add("count", info.PageSize);

                var query = new GraphQLQuery
                {
                    Query = queryString,
                    Variables = vars,

                };
                return GetResponseAsync<IPagedData<T>>(CreateRequest(query), ct);
            }

            return GetPageAsync;
        }

        private Func<PagingInfo, CancellationToken, Task<OneOf<IPagedData<T>, IAniListError>>> CreateGetPageFunc<T, TResponse>(string queryString,
            object variables, Func<TResponse, IPagedData<T>> responseSelector) where TResponse : class
        {
            Task<OneOf<IPagedData<T>, IAniListError>> GetPageAsync(PagingInfo info, CancellationToken ct)
            {
                var vars = JObject.FromObject(variables ?? new object(), JsonNetSerializer.Default.Serializer);
                vars.Add("page", info.Page);
                vars.Add("count", info.PageSize);

                var query = new GraphQLQuery
                {
                    Query = queryString,
                    Variables = vars,

                };
                return GetResponseAsync(CreateRequest(query), responseSelector, ct);
            }

            return GetPageAsync;
        }

        private static bool HasNextPage<T>(PagingInfo info, IPagedData<T> data) => data.PageInfo.HasNextPage;

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
                    DefaultValueHandling = DefaultValueHandling.Include,
                    ContractResolver = AniListContractResolver.Instance,
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
                using (var jsonTextWriter = new JsonTextWriter(stringWriter))
                {
#if DEBUG
                    jsonTextWriter.Formatting = Formatting.Indented;
#endif

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

            public static JsonNetSerializer Default => new JsonNetSerializer();
        }

#endregion
    }
}
