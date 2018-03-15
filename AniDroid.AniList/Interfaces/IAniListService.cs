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

        Task<IRestResponse<AniListAuthorizationResponse>> AuthenticateUser(IAniListAuthConfig config, string code, CancellationToken cToken);

        #endregion

        #region Media

        Task<OneOf<Media, IAniListError>> GetMediaById(int mediaId, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> SearchMedia(string queryText, Media.MediaType type, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Media>, IAniListError>> BrowseMedia(BrowseMediaDto browseDto, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Character.Edge>, IAniListError>> GetMediaCharacters(int mediaId, int perPage);

        IAsyncEnumerable<OneOf<IPagedData<Staff.Edge>, IAniListError>> GetMediaStaff(int mediaId, int perPage);

        #endregion

        #region User

        Task<OneOf<User, IAniListError>> GetUser(string userName, int? userId, CancellationToken cToken);

        Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<User>, IAniListError>> SearchUsers(string queryText, int perPage);

        Task<OneOf<User.UserFavourites, IAniListError>> ToggleFavorite(int id, User.FavoriteType favoriteType, CancellationToken cToken);

        #endregion

        #region Activity

        Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken);

        IAsyncEnumerable<OneOf<IPagedData<AniListActivity>, IAniListError>> GetAniListActivity(int perPage);

        IAsyncEnumerable<OneOf<IPagedData<AniListNotification>, IAniListError>> GetAniListNotifications(int perPage);

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

        #endregion

    }
}
