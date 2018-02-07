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
        Task<IRestResponse<GraphQLResponse<Media>>> GetMedia(int id, Media.MediaType type, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<User>>> GetUser(string name, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<Media.MediaListCollection>>> GetUserMediaList(string userName, Media.MediaType type, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<AniListObject.PagedData<List<AniListActivity>>>>> GetAniListActivity(int page, int count, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<AniListActivity>>> PostTextActivity(string text, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<AniListActivity.ActivityReply>>> PostActivityReply(int activityId, string text, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<List<User>>>> ToggleLike(int id, AniListObject.LikeableType type, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<AniListActivity>>> GetAniListActivityById(int id, CancellationToken cToken = default(CancellationToken));
        Task<IRestResponse<GraphQLResponse<AniListObject.PagedData<List<AniListNotification>>>>> GetAniListNotifications(int page, int count, CancellationToken cToken = default(CancellationToken));
    }
}
