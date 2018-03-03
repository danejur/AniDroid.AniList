using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace AniDroid.AniList.Models
{
    public class AniListActivity : AniListObject
    {
        public ActivityType Type { get; set; }
        public string SiteUrl { get; set; }
        public int CreatedAt { get; set; }
        public int ReplyCount { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<ActivityReply> Replies { get; set; }
        public List<User> Likes { get; set; }

        /*--- Message Activity ---*/
        public int RecipientId { get; set; }
        public int MessengerId { get; set; }
        public string Message { get; set; }
        public User Recipient { get; set; }
        public User Messenger { get; set; }

        /*--- Text Activity ---*/
        public string Text { get; set; }

        /*--- List Activity ---*/
        public string Status { get; set; }
        public string Progress { get; set; }
        public Media Media { get; set; }

        #region Internal Classes

        public class ActivityReply : AniListObject
        {
            public int UserId { get; set; }
            public int ActivityId { get; set; }
            public string Text { get; set; }
            public int CreatedAt { get; set; }
            public User User { get; set; }
            public List<User> Likes { get; set; }
        }

        #endregion

        #region Enum Classes

        [JsonConverter(typeof(AniListEnumConverter<ActivityType>))]
        public sealed class ActivityType : AniListEnum
        {
            private ActivityType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static ActivityType Text { get; } = new ActivityType("TEXT", "Text", 0);
            public static ActivityType AnimeList { get; } = new ActivityType("ANIME_LIST", "Anime List", 0);
            public static ActivityType MangaList { get; } = new ActivityType("MANGA_LIST", "Manga List", 0);
            public static ActivityType Message { get; } = new ActivityType("MESSAGE", "Message", 0);
            public static ActivityType MediaList { get; } = new ActivityType("MEDIA_LIST", "Media List", 0);
        }

        #endregion
    }
}
