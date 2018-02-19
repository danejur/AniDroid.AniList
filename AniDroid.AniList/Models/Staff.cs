using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.Interfaces;

namespace AniDroid.AniList.Models
{
    public class Staff : AniListObject
    {
        public AniListName Name { get; set; }
        public string Language { get; set; }
        public AniListImage Image { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public string SiteUrl { get; set; }
        public IPagedData<Media.Edge> StaffMedia { get; set; }
        public IPagedData<Character.Edge> Characters { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Staff>
        {
            public string Role { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion

        #region Enum Classes

        public sealed class StaffLanguage : AniListEnum
        {
            protected StaffLanguage(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static StaffLanguage Japanese => new StaffLanguage("JAPANESE", "Japanese", 0);
            public static StaffLanguage English => new StaffLanguage("ENGLISH", "English", 1);
            public static StaffLanguage Korean => new StaffLanguage("KOREAN", "Korean", 2);
            public static StaffLanguage Italian => new StaffLanguage("ITALIAN", "Japanese", 3);
            public static StaffLanguage Spanish => new StaffLanguage("SPANISH", "Spanish", 4);
            public static StaffLanguage Portuguese => new StaffLanguage("PORTUGUESE", "Portuguese", 5);
            public static StaffLanguage French => new StaffLanguage("FRENCH", "French", 6);
            public static StaffLanguage German => new StaffLanguage("GERMAN", "German", 7);
            public static StaffLanguage Hebrew => new StaffLanguage("HEBREW", "Hebrew", 8);
            public static StaffLanguage Hungarian => new StaffLanguage("HUNGARIAN", "Hungarian", 9);
        }

        #endregion
    }
}
