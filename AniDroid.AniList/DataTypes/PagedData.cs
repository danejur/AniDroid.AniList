using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.Interfaces;
using AniDroid.AniList.Models;

namespace AniDroid.AniList.DataTypes
{
    public class PagedData<T> : IPagedData<T>
    {
        public PageInfo PageInfo { get; set; }
        public ICollection<T> Data { get; set; }
    }
}
