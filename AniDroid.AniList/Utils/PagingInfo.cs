using System;

namespace AniDroid.AniList.Utils
{
    public sealed class PagingInfo
    {
        public Int32 Page { get; set; } = 1;
        public Int32 PageSize { get; set; }
        public Int32? Remaining { get; set; }

        public PagingInfo(Int32 pageSize)
        {
            PageSize = pageSize;
        }
    }
}
