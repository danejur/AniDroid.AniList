using AniDroid.AniList.Interfaces;
using AniDroid.AniList.Models;
using OneOf;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListService
    {
        IAniListServiceConfig Config { get; }
        IAuthCodeResolver AuthCodeResolver { get; }

        #region Media

        Task<OneOf<Media, IAniListError>> GetMedia(int id, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Media>> SearchMediaPaging(string queryText, Media.MediaType type, int perPage);

        #endregion

        #region User

        Task<OneOf<User, IAniListError>> GetUser(string name, CancellationToken cToken);

        Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<User>> SearchUsersPaging(string queryText, int perPage);

        #endregion

        #region Activity

        IAsyncEnumerable<IPagedData<AniListActivity>> GetAniListActivityPaging(int perPage);

        Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<AniListNotification>> GetAniListNotificationsPaging(int perPage);

        #endregion

        #region Character

        IAsyncEnumerable<IPagedData<Character>> SearchCharactersPaging(string queryText, int perPage);

        Task<OneOf<Character, IAniListError>> GetCharacterById(int id, CancellationToken cToken);

        #endregion

        #region Staff

        IAsyncEnumerable<IPagedData<Staff>> SearchStaffPaging(string queryText, int perPage);

        Task<OneOf<Staff, IAniListError>> GetStaffById(int id, CancellationToken cToken);

        IAsyncEnumerable<IPagedData<Character.Edge>> GetStaffCharactersPaging(int staffId, int perPage);

        #endregion

        #region Studio

        IAsyncEnumerable<IPagedData<Studio>> SearchStudiosPaging(string queryText, int perPage);

        #endregion

        #region ForumThread

        IAsyncEnumerable<IPagedData<ForumThread>> SearchForumThreadsPaging(string queryText, int perPage);

        #endregion

    }
}
