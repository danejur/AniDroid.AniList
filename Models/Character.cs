using AniDroid.AniList.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Character : BaseAniListObject
    {
        public AniListName Name { get; set; }
        public AniListImage Image { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public string SiteUrl { get; set; }
        public Connection<Media.Edge, Media> Media { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Character>
        {
            public string Role { get; set; }
            //TODO: voice actors
            public List<Media> Media { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion

        #region Enum Classes

        public class CharacterRole : AniListEnum
        {
            protected CharacterRole(string val, string displayVal) : base(val, displayVal) { }

            public static CharacterRole Main => new CharacterRole("MAIN", "Main");
            public static CharacterRole Supporting => new CharacterRole("SUPPORTING", "Supporting");
            public static CharacterRole Background => new CharacterRole("BACKGROUND", "Background");
        }

        #endregion
    }
}
