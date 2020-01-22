using AniDroid.AniList.Interfaces;
using AniDroid.AniList.Models;
using OneOf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AniDroid.AniList.Dto;
using AniDroid.AniList.Service;

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListService
    {
        IAniListServiceConfig Config { get; }
        IAuthCodeResolver AuthCodeResolver { get; }

        #region Authorization

        Task<OneOf<IRestResponse<AniListAuthorizationResponse>, IAniListError>> AuthenticateUser(IAniListAuthConfig config, string code, CancellationToken cToken);

        #endregion

        #region Media

        Task<OneOf<Media, IAniListError>> GetMediaById(int mediaId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> SearchMedia(string queryText, Media.MediaType type, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> BrowseMedia(BrowseMediaDto browseDto, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Character.Edge>, IAniListError>> GetMediaCharacters(int mediaId, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Staff.Edge>, IAniListError>> GetMediaStaff(int mediaId, int perPage);

        Task<OneOf<Media.MediaList, IAniListError>> UpdateMediaListEntry(MediaListEditDto editDto, CancellationToken cToken);

        Task<OneOf<bool, IAniListError>> DeleteMediaListEntry(int mediaListId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Review>, IAniListError>> GetMediaReviews(int mediaId, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Recommendation.Edge>, IAniListError>> GetMediaRecommendations(int mediaId, int perPage);

        Task<OneOf<IList<Media.MediaTag>, IAniListError>> GetMediaTagCollectionAsync(CancellationToken cToken);

        Task<OneOf<IList<string>, IAniListError>> GetGenreCollectionAsync(CancellationToken cToken);

        #endregion

        #region User

        Task<OneOf<User, IAniListError>> GetCurrentUser(CancellationToken cToken);

        Task<OneOf<User, IAniListError>> GetUser(string userName, int? userId, CancellationToken cToken);

        Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(int userId, Media.MediaType type, bool groupCompleted, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> SearchUsers(string queryText, int perPage);

        Task<OneOf<User.UserFavourites, IAniListError>> ToggleFavorite(FavoriteDto favoriteDto, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> PostUserMessage(int userId, string message, CancellationToken cToken);

        Task<OneOf<User, IAniListError>> ToggleFollowUser(int userId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> GetUserFollowers(int userId, User.UserSort sort, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> GetUserFollowing(int userId, User.UserSort sort, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media.MediaList>, IAniListError>> GetMediaFollowingUsersMediaLists(int mediaId, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Review>, IAniListError>> GetUserReviews(int userId, int perPage);

        #endregion

        #region Activity

        /// <summary>
        /// Saves a text activity to AniList, which will show up in the user's feed.
        /// </summary>
        /// <param name="text">The text for the activity.</param>
        /// <param name="activityId">(Optional) The Id of the activity to update with new text.</param>
        /// <param name="cToken"></param>
        /// <returns></returns>
        Task<OneOf<AniListActivity, IAniListError>> SaveTextActivity(string text, int? activityId, CancellationToken cToken);

        Task<OneOf<AniListObject.DeletedResponse, IAniListError>> DeleteActivity(int activityId, CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<AniListActivity>, IAniListError>> GetAniListActivity(AniListActivityDto activityDto, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<AniListNotification>, IAniListError>> GetAniListNotifications(bool resetNotificationCount, int perPage);

        Task<OneOf<User, IAniListError>> GetAniListNotificationCount(CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> SaveActivityReply(int id, string text, CancellationToken cToken);

        Task<OneOf<AniListObject.DeletedResponse, IAniListError>> DeleteActivityReply(int id, CancellationToken cToken);

        #endregion

        #region Character

        Task<OneOf<Character, IAniListError>> GetCharacterById(int characterId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Character>, IAniListError>> SearchCharacters(string queryText, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetCharacterMedia(int characterId, Media.MediaType mediaType, int perPage);

        #endregion

        #region Staff

        Task<OneOf<Staff, IAniListError>> GetStaffById(int staffId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Staff>, IAniListError>> SearchStaff(string queryText, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Character.Edge>, IAniListError>> GetStaffCharacters(int staffId, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetStaffMedia(int staffId, Media.MediaType mediaType, int perPage);

        #endregion

        #region Studio

        Task<OneOf<Studio, IAniListError>> GetStudioById(int studioId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Studio>, IAniListError>> SearchStudios(string queryText, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media.Edge>, IAniListError>> GetStudioMedia(int studioId, int perPage);

        #endregion

        #region ForumThread

        IAsyncEnumerable<OneOf<IPagedData<ForumThread>, IAniListError>> SearchForumThreads(string queryText, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<ForumThread>, IAniListError>> GetMediaForumThreads(int mediaId, int perPage);

        #endregion

    }
}
