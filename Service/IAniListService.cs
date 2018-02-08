using AniDroid.AniList.Interfaces;
using AniDroid.AniList.Models;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AniDroid.AniList.Service
{
    public interface IAniListService
    {
        IAniListServiceConfig Config { get; }
        IAuthCodeResolver AuthCodeResolver { get; }

        #region Media

        Task<IAniListServiceResponse<Media>> GetMedia(int id, Media.MediaType type, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListObject.PagedData<List<Media>>>> SearchMedia(string queryText, int page, int count, Media.MediaType type = null, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region User

        Task<IAniListServiceResponse<User>> GetUser(string name, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<Media.MediaListCollection>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListObject.PagedData<List<User>>>> SearchUsers(string queryText, int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region Activity

        Task<IAniListServiceResponse<AniListObject.PagedData<List<AniListActivity>>>> GetAniListActivity(int page, int count, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListActivity>> PostTextActivity(string text, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListActivity.ActivityReply>> PostActivityReply(int activityId, string text, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<List<User>>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListActivity>> GetAniListActivityById(int id, CancellationToken cToken = default(CancellationToken));
        Task<IAniListServiceResponse<AniListObject.PagedData<List<AniListNotification>>>> GetAniListNotifications(int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region Character

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Character>>>> SearchCharacters(string queryText, int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region Staff

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Staff>>>> SearchStaff(string queryText, int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region Studio

        Task<IAniListServiceResponse<AniListObject.PagedData<List<Studio>>>> SearchStudios(string queryText, int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

        #region ForumThread

        Task<IAniListServiceResponse<AniListObject.PagedData<List<ForumThread>>>> SearchForumThreads(string queryText, int page, int count, CancellationToken cToken = default(CancellationToken));

        #endregion

    }
}
