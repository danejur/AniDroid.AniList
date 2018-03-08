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

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListService
    {
        IAniListServiceConfig Config { get; }
        IAuthCodeResolver AuthCodeResolver { get; }

        #region Media

        Task<OneOf<Media, IAniListError>> GetMediaById(int mediaId, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Media>> SearchMedia(string queryText, Media.MediaType type, int perPage);

        IAsyncEnumerable<IPagedData<Media>> BrowseMedia(BrowseMediaDto browseDto, int perPage);

        IAsyncEnumerable<IPagedData<Character.Edge>> GetMediaCharacters(int mediaId, int perPage);

        IAsyncEnumerable<IPagedData<Staff.Edge>> GetMediaStaff(int mediaId, int perPage);

        #endregion

        #region User

        Task<OneOf<User, IAniListError>> GetUser(string userName, int? userId, CancellationToken cToken);

        Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<User>> SearchUsers(string queryText, int perPage);

        Task<OneOf<User.UserFavourites, IAniListError>> ToggleFavorite(int id, User.FavoriteType favoriteType, CancellationToken cToken);

        #endregion

        #region Activity

        Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<AniListActivity>> GetAniListActivity(int perPage);

        IAsyncEnumerable<IPagedData<AniListNotification>> GetAniListNotifications(int perPage);

        #endregion

        #region Character

        Task<OneOf<Character, IAniListError>> GetCharacterById(int characterId, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Character>> SearchCharacters(string queryText, int perPage);

        IAsyncEnumerable<IPagedData<Media.Edge>> GetCharacterMedia(int characterId, Media.MediaType mediaType, int perPage);

        #endregion

        #region Staff

        Task<OneOf<Staff, IAniListError>> GetStaffById(int staffId, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Staff>> SearchStaff(string queryText, int perPage);

        IAsyncEnumerable<IPagedData<Character.Edge>> GetStaffCharacters(int staffId, int perPage);

        IAsyncEnumerable<IPagedData<Media.Edge>> GetStaffMedia(int staffId, Media.MediaType mediaType, int perPage);

        #endregion

        #region Studio

        Task<OneOf<Studio, IAniListError>> GetStudioById(int studioId, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Studio>> SearchStudios(string queryText, int perPage);

        IAsyncEnumerable<IPagedData<Media.Edge>> GetStudioMedia(int studioId, int perPage);

        #endregion

        #region ForumThread

        IAsyncEnumerable<IPagedData<ForumThread>> SearchForumThreads(string queryText, int perPage);

        #endregion

    }
}
