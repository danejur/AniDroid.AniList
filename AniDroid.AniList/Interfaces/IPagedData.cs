using System.Collections.Generic;
using AniDroid.AniList.Models;

namespace AniDroid.AniList.Interfaces
{
    public interface IPagedData<T>
    {
        AniListObject.PageInfo PageInfo { get; set; }
        ICollection<T> Data { get; set; }
    }
}
