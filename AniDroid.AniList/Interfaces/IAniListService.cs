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

        Task<OneOf<AniListObject.PagedData<List<Media>>, IAniListError>> SearchMedia(string queryText, int page, int count, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Media>>> SearchMediaPaging(string queryText, Media.MediaType type, int perPage);

        #endregion

        #region User

        Task<OneOf<User, IAniListError>> GetUser(string name, CancellationToken cToken);

        Task<OneOf<Media.MediaListCollection, IAniListError>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken);

        Task<OneOf<AniListObject.PagedData<List<User>>, IAniListError>> SearchUsers(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<User>>> SearchUsersPaging(string queryText, int perPage);

        #endregion

        #region Activity

        Task<OneOf<AniListObject.PagedData<List<AniListActivity>>, IAniListError>> GetAniListActivity(int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<AniListActivity>>> GetAniListActivityPaging(int perPage);

        Task<OneOf<AniListActivity, IAniListError>> PostTextActivity(string text, CancellationToken cToken);

        Task<OneOf<AniListActivity.ActivityReply, IAniListError>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<OneOf<List<User>, IAniListError>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<OneOf<AniListActivity, IAniListError>> GetAniListActivityById(int id, CancellationToken cToken);

        Task<OneOf<AniListObject.PagedData<List<AniListNotification>>, IAniListError>> GetAniListNotifications(int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<AniListNotification>>> GetAniListNotificationsPaging(int perPage);

        #endregion

        #region Character

        Task<OneOf<AniListObject.PagedData<List<Character>>, IAniListError>> SearchCharacters(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Character>>> SearchCharactersPaging(string queryText, int perPage);

        Task<OneOf<Character, IAniListError>> GetCharacterById(int id, CancellationToken cToken);

        #endregion

        #region Staff

        Task<OneOf<AniListObject.PagedData<List<Staff>>, IAniListError>> SearchStaff(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Staff>>> SearchStaffPaging(string queryText, int perPage);

        #endregion

        #region Studio

        Task<OneOf<AniListObject.PagedData<List<Studio>>, IAniListError>> SearchStudios(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Studio>>> SearchStudiosPaging(string queryText, int perPage);

        #endregion

        #region ForumThread

        Task<OneOf<AniListObject.PagedData<List<ForumThread>>, IAniListError>> SearchForumThreads(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<ForumThread>>> SearchForumThreadsPaging(string queryText, int perPage);

        #endregion

    }
}
