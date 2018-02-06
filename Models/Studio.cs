using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Studio : AniListObject
    {
        public string Name { get; set; }
        public Connection<Media.Edge, Media> Media { get; set; }
        public string SiteUrl { get; set; }
        public bool IsFavourite { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Studio>
        {
            public bool IsMain { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion
    }
}
