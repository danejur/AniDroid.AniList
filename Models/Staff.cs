using AniDroid.AniList.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Staff : BaseAniListObject
    {
        public AniListName Name { get; set; }
        public string Language { get; set; }
        public AniListImage Image { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public string SiteUrl { get; set; }
        public Connection<Media.Edge, Media> StaffMedia { get; set; }
        public Connection<Character.Edge, Character> Characters { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Staff>
        {
            public string Role { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion

        #region Enum Classes

        public class StaffLanguage : AniListEnum
        {
            protected StaffLanguage(string val, string displayVal) : base(val, displayVal) { }

            public static StaffLanguage Japanese => new StaffLanguage("JAPANESE", "Japanese");
            public static StaffLanguage English => new StaffLanguage("ENGLISH", "English");
            public static StaffLanguage Korean => new StaffLanguage("KOREAN", "Korean");
            public static StaffLanguage Italian => new StaffLanguage("ITALIAN", "Japanese");
            public static StaffLanguage Spanish => new StaffLanguage("SPANISH", "Spanish");
            public static StaffLanguage Portuguese => new StaffLanguage("PORTUGUESE", "Portuguese");
            public static StaffLanguage French => new StaffLanguage("FRENCH", "French");
            public static StaffLanguage German => new StaffLanguage("GERMAN", "German");
            public static StaffLanguage Hebrew => new StaffLanguage("HEBREW", "Hebrew");
            public static StaffLanguage Hungarian => new StaffLanguage("HUNGARIAN", "Hungarian");
        }

        #endregion
    }
}
