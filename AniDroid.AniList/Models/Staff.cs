using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.Interfaces;
using Newtonsoft.Json;

namespace AniDroid.AniList.Models
{
    public class Staff : AniListObject
    {
        public AniListName Name { get; set; }
        public StaffLanguage Language { get; set; }
        public AniListImage Image { get; set; }
        public string Description { get; set; }
        public bool IsFavourite { get; set; }
        public string SiteUrl { get; set; }
        public Connection<Media.Edge, Media> StaffMedia { get; set; }
        public Connection<Character.Edge, Character> Characters { get; set; }
        public IPagedData<Media.Edge> Anime { get; set; }
        public IPagedData<Media.Edge> Manga { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Staff>
        {
            public string Role { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion

        #region Enum Classes

        [JsonConverter(typeof(AniListEnumConverter<StaffLanguage>))]
        public sealed class StaffLanguage : AniListEnum
        {
            private StaffLanguage(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static StaffLanguage Japanese { get; } = new StaffLanguage("JAPANESE", "Japanese", 0);
            public static StaffLanguage English { get; } = new StaffLanguage("ENGLISH", "English", 1);
            public static StaffLanguage Korean { get; } = new StaffLanguage("KOREAN", "Korean", 2);
            public static StaffLanguage Italian { get; } = new StaffLanguage("ITALIAN", "Japanese", 3);
            public static StaffLanguage Spanish { get; } = new StaffLanguage("SPANISH", "Spanish", 4);
            public static StaffLanguage Portuguese { get; } = new StaffLanguage("PORTUGUESE", "Portuguese", 5);
            public static StaffLanguage French { get; } = new StaffLanguage("FRENCH", "French", 6);
            public static StaffLanguage German { get; } = new StaffLanguage("GERMAN", "German", 7);
            public static StaffLanguage Hebrew { get; } = new StaffLanguage("HEBREW", "Hebrew", 8);
            public static StaffLanguage Hungarian { get; } = new StaffLanguage("HUNGARIAN", "Hungarian", 9);
        }

        #endregion
    }
}
