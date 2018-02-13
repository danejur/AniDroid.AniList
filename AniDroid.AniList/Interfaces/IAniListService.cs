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

        Task<OneOf<IAniListError, Media>> GetMedia(int id, Media.MediaType type, CancellationToken cToken);

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Media>>>> SearchMedia(string queryText, int page, int count, Media.MediaType type, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Media>>> SearchMediaPaging(string queryText, Media.MediaType type, int perPage);

        #endregion

        #region User

        Task<OneOf<IAniListError, User>> GetUser(string name, CancellationToken cToken);

        Task<OneOf<IAniListError, Media.MediaListCollection>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken);

        Task<OneOf<IAniListError, AniListObject.PagedData<List<User>>>> SearchUsers(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<User>>> SearchUsersPaging(string queryText, int perPage);

        #endregion

        #region Activity

        Task<IAniListServiceResponse<AniListObject.PagedData<List<AniListActivity>>>> GetAniListActivity(int page, int count, CancellationToken cToken);

        Task<IAniListServiceResponse<AniListActivity>> PostTextActivity(string text, CancellationToken cToken);

        Task<IAniListServiceResponse<AniListActivity.ActivityReply>> PostActivityReply(int activityId, string text, CancellationToken cToken);

        Task<IAniListServiceResponse<List<User>>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken);

        Task<IAniListServiceResponse<AniListActivity>> GetAniListActivityById(int id, CancellationToken cToken);

        Task<IAniListServiceResponse<AniListObject.PagedData<List<AniListNotification>>>> GetAniListNotifications(int page, int count, CancellationToken cToken);

        #endregion

        #region Character

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Character>>>> SearchCharacters(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Character>>> SearchCharactersPaging(string queryText, int perPage);

        Task<IAniListServiceResponse<Character>> GetCharacterById(int id, CancellationToken cToken);

        #endregion

        #region Staff

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Staff>>>> SearchStaff(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Staff>>> SearchStaffPaging(string queryText, int perPage);

        #endregion

        #region Studio

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Studio>>>> SearchStudios(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<Studio>>> SearchStudiosPaging(string queryText, int perPage);

        #endregion

        #region ForumThread

        Task<IAniListServiceResponse<AniListObject.PagedData<List<ForumThread>>>> SearchForumThreads(string queryText, int page, int count, CancellationToken cToken);

        IAsyncEnumerable<AniListObject.PagedData<ICollection<ForumThread>>> SearchForumThreadsPaging(string queryText, int perPage);

        #endregion

    }
}
