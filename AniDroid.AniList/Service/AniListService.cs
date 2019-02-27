using System;
using System.Collections.Generic;
using RestSharp;
using System.Threading.Tasks;
using System.Threading;
using AniDroid.AniList.Models;
using System.IO;
using AniDroid.AniList.Dto;
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

        #region Authorization

        public async Task<OneOf<IRestResponse<AniListAuthorizationResponse>, IAniListError>> AuthenticateUser(IAniListAuthConfig config, string code, CancellationToken cToken)
        {
            var authReq = new RestRequest(Method.POST);
            authReq.AddParameter("client_id", config.ClientId);
            authReq.AddParameter("client_secret", config.ClientSecret);
            authReq.AddParameter("grant_type", "authorization_code");
            authReq.AddParameter("redirect_uri", config.RedirectUri);
            authReq.AddParameter("code", code);

            var client = new RestClient(config.AuthTokenUri);

            try
            {
                return new OneOf<IRestResponse<AniListAuthorizationResponse>, IAniListError>(
                    await client.ExecuteTaskAsync<AniListAuthorizationResponse>(authReq, cToken));
            }
            catch (Exception e)
            {
                return new AniListError(0, e.Message, e, null);
            }
        }

        #endregion

        #region Media

        public Task<OneOf<Media, IAniListError>> GetMediaById(int mediaId, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaById,
                Variables = new { mediaId },
            };
            return GetResponseAsync<Media>(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> SearchMedia(string queryText,
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

        public IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> BrowseMedia(BrowseMediaDto browseDto, int perPage)
        {
            return new PagedAsyncEnumerable<Media>(perPage,
                CreateGetPageFunc<Media>(QueryStore.BrowseMedia, browseDto),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Character.Edge>, IAniListError>> GetMediaCharacters(int mediaId, int perPage)
        {
            var arguments = new { mediaId };
            return new PagedAsyncEnumerable<Character.Edge>(perPage,
                CreateGetPageFunc<Character.Edge, Media>(QueryStore.GetMediaCharacters, arguments, media => media.Characters),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Staff.Edge>, IAniListError>> GetMediaStaff(int mediaId, int perPage)
        {
            var arguments = new { mediaId };
            return new PagedAsyncEnumerable<Staff.Edge>(perPage,
                CreateGetPageFunc<Staff.Edge, Media>(QueryStore.GetMediaStaff, arguments, media => media.Staff),
                HasNextPage);
        }

        public Task<OneOf<Media.MediaList, IAniListError>> UpdateMediaListEntry(MediaListEditDto editDto, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.UpdateMediaList,
                Variables = editDto
            };
            return GetResponseAsync<Media.MediaList>(query, cToken);
        }

        public async Task<OneOf<bool, IAniListError>> DeleteMediaListEntry(int mediaListId, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.DeleteMediaList,
                Variables = new { mediaListId }
            };

            return await GetResponseAsync(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Review>, IAniListError>> GetMediaReviews(int mediaId, int perPage)
        {
            var arguments = new { mediaId };
            return new PagedAsyncEnumerable<Review>(perPage,
                CreateGetPageFunc<Review>(QueryStore.GetMediaReviews, arguments), HasNextPage);
        }

        public async Task<OneOf<IList<Media.MediaTag>, IAniListError>> GetMediaTagCollectionAsync(CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaTagCollection
            };

            return await GetResponseAsync<IList<Media.MediaTag>>(query, cToken);
        }

        public async Task<OneOf<IList<string>, IAniListError>> GetGenreCollectionAsync(CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetGenreCollection
            };

            return await GetResponseAsync<IList<string>>(query, cToken);
        }

        #endregion

        #region User

        public Task<OneOf<User, IAniListError>> GetCurrentUser(CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetCurrentUser
            };
            return GetResponseAsync<User>(query, cToken);
        }

        public Task<OneOf<User, IAniListError>> GetUser(string userName, int? userId, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUser,
                Variables = new { userName, userId },
            };
            return GetResponseAsync<User>(query, cToken);
        }

        public Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(int userId, Media.MediaType type, bool groupCompleted, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetMediaListsByUserIdAndType,
                Variables = new { userId, type, groupCompleted },
            };
            return GetResponseAsync<Media.MediaListCollection>(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> SearchUsers(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<User>(perPage,
                CreateGetPageFunc<User>(QueryStore.SearchUsers, arguments),
                HasNextPage);
        }

        public Task<OneOf<User.UserFavourites, IAniListError>> ToggleFavorite(FavoriteDto favoriteDto, CancellationToken cToken)
        {
            var mutation = new GraphQLQuery
            {
                Query = QueryStore.ToggleUserFavorite,
                Variables = favoriteDto,
            };
            return GetResponseAsync<User.UserFavourites>(mutation, cToken);
        }

        public Task<OneOf<AniListActivity, IAniListError>> PostUserMessage(int userId, string message, CancellationToken cToken)
        {
            var mutation = new GraphQLQuery
            {
                Query = QueryStore.PostUserMessage,
                Variables = new { userId, message }
            };
            return GetResponseAsync<AniListActivity>(mutation, cToken);
        }

        public Task<OneOf<User, IAniListError>> ToggleFollowUser(int userId, CancellationToken cToken)
        {
            var mutation = new GraphQLQuery
            {
                Query = QueryStore.ToggleUserFollowing,
                Variables = new { userId }
            };
            return GetResponseAsync<User>(mutation, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> GetUserFollowers(int userId, User.UserSort sort,
            int perPage = 20)
        {
            var arguments = new
            {
                userId,
                sort
            };
            return new PagedAsyncEnumerable<User>(perPage,
                CreateGetPageFunc<User>(QueryStore.GetUserFollowers, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> GetUserFollowing(int userId, User.UserSort sort,
            int perPage = 20)
        {
            var arguments = new
            {
                userId,
                sort
            };
            return new PagedAsyncEnumerable<User>(perPage,
                CreateGetPageFunc<User>(QueryStore.GetUserFollowing, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Media.MediaList>, IAniListError>> GetMediaFollowingUsersMediaLists(int mediaId, int perPage)
        {
            var arguments = new
            {
                mediaId
            };
            return new PagedAsyncEnumerable<Media.MediaList>(perPage,
                CreateGetPageFunc<Media.MediaList>(QueryStore.GetMediaFollowingUsersMediaLists, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Review>, IAniListError>> GetUserReviews(int userId, int perPage)
        {
            var arguments = new { userId };
            return new PagedAsyncEnumerable<Review>(perPage,
                CreateGetPageFunc<Review>(QueryStore.GetUserReviews, arguments), HasNextPage);
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

        public Task<OneOf<AniListActivity, IAniListError>> SaveTextActivity(string text, int? activityId, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SaveTextActivity,
                Variables = new { text, activityId },
            };
            return GetResponseAsync<AniListActivity>(query, cToken);
        }

        public Task<OneOf<AniListObject.DeletedResponse, IAniListError>> DeleteActivity(int activityId, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.DeleteActivity,
                Variables = new { activityId },
            };
            return GetResponseAsync<AniListObject.DeletedResponse>(query, cToken);
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

        public IAsyncEnumerable<OneOf<IPagedData<AniListActivity>, IAniListError>> GetAniListActivity(AniListActivityDto activityDto, int perPage = 20)
        {
            return new PagedAsyncEnumerable<AniListActivity>(perPage,
                CreateGetPageFunc<AniListActivity>(QueryStore.GetAniListActivity, activityDto),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<AniListNotification>, IAniListError>> GetAniListNotifications(bool resetNotificationCount, int perPage = 20)
        {
            return new PagedAsyncEnumerable<AniListNotification>(perPage,
                CreateGetPageFunc<AniListNotification>(QueryStore.GetUserNotifications, new { resetNotificationCount }),
                HasNextPage);
        }

        public Task<OneOf<User, IAniListError>> GetAniListNotificationCount(CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetUserNotificationCount
            };
            return GetResponseAsync<User>(query, cToken);
        }

        public Task<OneOf<AniListActivity.ActivityReply, IAniListError>> SaveActivityReply(int id, string text, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.SaveActivityReply,
                Variables = new { id, text },
            };
            return GetResponseAsync<AniListActivity.ActivityReply>(query, cToken);
        }

        public Task<OneOf<AniListObject.DeletedResponse, IAniListError>> DeleteActivityReply(int id, CancellationToken cToken)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.DeleteActivityReply,
                Variables = new { id },
            };
            return GetResponseAsync<AniListObject.DeletedResponse>(query, cToken);
        }

        #endregion

        #region Character

        public Task<OneOf<Character, IAniListError>> GetCharacterById(int characterId, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetCharacterById,
                Variables = new { characterId },
            };
            return GetResponseAsync<Character>(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Character>, IAniListError>> SearchCharacters(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Character>(perPage,
                CreateGetPageFunc<Character>(QueryStore.SearchCharacters, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetCharacterMedia(int characterId, Media.MediaType mediaType, int perPage = 20)
        {
            var arguments = new { characterId, mediaType = mediaType?.Value };
            return new PagedAsyncEnumerable<Media.Edge>(perPage,
                CreateGetPageFunc<Media.Edge, Character>(QueryStore.GetCharacterMedia, arguments, character => character.Media),
                HasNextPage);
        }

        #endregion

        #region Staff

        public Task<OneOf<Staff, IAniListError>> GetStaffById(int staffId, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetStaffById,
                Variables = new { staffId },
            };
            return GetResponseAsync<Staff>(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Staff>, IAniListError>> SearchStaff(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Staff>(perPage,
                CreateGetPageFunc<Staff>(QueryStore.SearchStaff, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Character.Edge>, IAniListError>> GetStaffCharacters(int staffId, int perPage = 20)
        {
            var arguments = new { staffId };
            return new PagedAsyncEnumerable<Character.Edge>(perPage,
                CreateGetPageFunc<Character.Edge, Staff>(QueryStore.GetStaffCharacters, arguments, staff => staff.Characters),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetStaffMedia(int staffId, Media.MediaType mediaType, int perPage = 20)
        {
            var arguments = new { staffId, mediaType = mediaType.Value };
            return new PagedAsyncEnumerable<Media.Edge>(perPage,
                CreateGetPageFunc<Media.Edge, Staff>(QueryStore.GetStaffMedia, arguments, staff => staff.StaffMedia),
                HasNextPage);
        }


        #endregion

        #region Studio

        public Task<OneOf<Studio, IAniListError>> GetStudioById(int studioId, CancellationToken cToken = default)
        {
            var query = new GraphQLQuery
            {
                Query = QueryStore.GetStudioById,
                Variables = new { studioId },
            };
            return GetResponseAsync<Studio>(query, cToken);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Studio>, IAniListError>> SearchStudios(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<Studio>(perPage,
                CreateGetPageFunc<Studio>(QueryStore.SearchStudios, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetStudioMedia(int studioId, int perPage = 20)
        {
            var arguments = new { studioId };
            return new PagedAsyncEnumerable<Media.Edge>(perPage,
                CreateGetPageFunc<Media.Edge, Studio>(QueryStore.GetStudioMedia, arguments, studio => studio.Media),
                HasNextPage);
        }

        #endregion

        #region ForumThread

        public IAsyncEnumerable<OneOf<IPagedData<ForumThread>, IAniListError>> SearchForumThreads(string queryText,
            int perPage = 20)
        {
            var arguments = new { queryText };
            return new PagedAsyncEnumerable<ForumThread>(perPage,
                CreateGetPageFunc<ForumThread>(QueryStore.SearchForumThreads, arguments),
                HasNextPage);
        }

        public IAsyncEnumerable<OneOf<IPagedData<ForumThread>, IAniListError>> GetMediaForumThreads(int mediaId, int perPage)
        {
            var arguments = new { mediaId };
            return new PagedAsyncEnumerable<ForumThread>(perPage,
                CreateGetPageFunc<ForumThread>(QueryStore.GetMediaForumThreads, arguments),
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
            client.AddHandler("*", AniListJsonSerializer.Default);

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
                JsonSerializer = AniListJsonSerializer.Default
            };
            req.AddJsonBody(query);
            return req;
        }

        // TODO: Document
        private async Task<OneOf<T, IAniListError>> GetResponseAsync<TResponse, T>(IRestRequest req, Func<TResponse, T> getObjectFunc, CancellationToken cToken) where T : class where TResponse : class
        {
            try
            {
                var servResp = await CreateClient().ExecuteTaskAsync<GraphQLResponse<TResponse>>(req, cToken)
                    .ConfigureAwait(false);

                if (servResp.IsSuccessful)
                {
                    return getObjectFunc(servResp.Data.Value);
                }

                return new AniListError((int)servResp.StatusCode, servResp.ErrorMessage, servResp.ErrorException,
                    servResp.Data?.Errors);
            }
            catch (Exception e)
            {
                return new AniListError(0, e.Message, e, null);
            }
        }

        // TODO: Document
        private Task<OneOf<T, IAniListError>> GetResponseAsync<T>(GraphQLQuery query, CancellationToken cToken)
            where T : class
        {
            return GetResponseAsync<T, T>(CreateRequest(query), obj => obj, cToken);
        }

        // TODO: Document
        private async Task<OneOf<bool, IAniListError>> GetResponseAsync(GraphQLQuery query, CancellationToken cToken)
        {
            try
            {
                var servResp = await CreateClient().ExecuteTaskAsync<GraphQLResponse>(CreateRequest(query), cToken)
                    .ConfigureAwait(false);

                if (servResp.IsSuccessful)
                {
                    return true;
                }

                return new AniListError((int)servResp.StatusCode, servResp.ErrorMessage, servResp.ErrorException,
                    servResp.Data?.Errors);
            }
            catch (Exception e)
            {
                return new AniListError(0, e.Message, e, null);
            }
        }

        // TODO: Document
        private Func<PagingInfo, CancellationToken, Task<OneOf<IPagedData<T>, IAniListError>>> CreateGetPageFunc<T>(string queryString,
            object variables)
        {
            return CreateGetPageFunc<T, IPagedData<T>>(queryString, variables, obj => obj);
        }

        // TODO: Document
        private Func<PagingInfo, CancellationToken, Task<OneOf<IPagedData<T>, IAniListError>>> CreateGetPageFunc<T, TResponse>(string queryString,
            object variables, Func<TResponse, IPagedData<T>> getPagedDataFunc) where TResponse : class
        {
            Task<OneOf<IPagedData<T>, IAniListError>> GetPageAsync(PagingInfo info, CancellationToken ct)
            {
                var vars = JObject.FromObject(variables ?? new object(), AniListJsonSerializer.Default.Serializer);
                vars.Add("page", info.Page);
                vars.Add("count", info.PageSize);

                var query = new GraphQLQuery
                {
                    Query = queryString,
                    Variables = vars,

                };
                return GetResponseAsync(CreateRequest(query), getPagedDataFunc, ct);
            }

            return GetPageAsync;
        }

        private static bool HasNextPage<T>(PagingInfo info, OneOf<IPagedData<T>, IAniListError> data) => data.Match((IAniListError error) => false)
            .Match(pagedData => pagedData?.PageInfo?.HasNextPage == true);

#endregion

    }
}
