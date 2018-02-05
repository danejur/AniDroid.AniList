using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AniDroid.AniListIntegration
{
    public static class AniListConstants
    {
        public static class SeriesType
        {
            public const string Anime = "anime";
            public const string Manga = "manga";
        }

        public static class Scoring
        {
            public static class Types
            {
                public const int TenPoint = 0;
                public const int HundredPoint = 1;
                public const int FiveStar = 2;
                public const int SmileyFace = 3;
                public const int TenPointDecimal = 4;
            }

            public static class DisplayTypes
            {
                public const string TenPoint = "10 Point";
                public const string HundredPoint = "100 Point";
                public const string FiveStar = "5 Star";
                public const string SmileyFace = ":) :| :(";
                public const string TenPointDecimal = "10.0 Point";
            }

            public static readonly Dictionary<int, string> TypeDisplayTypeDictionary = new Dictionary<int, string>
            {
                { Types.TenPoint, DisplayTypes.TenPoint },
                { Types.HundredPoint, DisplayTypes.HundredPoint },
                { Types.FiveStar, DisplayTypes.FiveStar },
                { Types.SmileyFace, DisplayTypes.SmileyFace },
                { Types.TenPointDecimal, DisplayTypes.TenPointDecimal }
            };

            public static readonly int[] SmileyFaceScores = new[] { 0, 35, 60, 85 };
            public static readonly string[] SmileyFaceDisplayScores = new[] { "", ":(", ":|", ":)" };
            public static readonly int[] FiveStarScores = new[] { 0, 10, 30, 50, 70, 90 };
            public static readonly string[] FiveStarDisplayScores = new[] { "", "★", "★★", "★★★", "★★★★", "★★★★★" };
            public static readonly string[] FiveStarSpinnerDisplayScores = new[] { "", "1 Star", "2 Stars", "3 Stars", "4 Stars", "5 Stars" };
        }

        public static class Search
        {
            public enum ItemSearchType
            {
                Anime = 2,
                Manga = 3,
                Character = 4,
                Staff = 5,
                Studio = 6,
                User = 7,
                Forum = 8
            }

            public class ItemSearchTypes
            {
                public const string Anime = "anime";
                public const string Manga = "manga";
                public const string Character = "character";
                public const string Staff = "staff";
                public const string Studio = "studio";
                public const string User = "user";
                public const string Forum = "forum";
            }
        }

        public static class Forum
        {
            public const string ForumThreadUrlTemplate = "http://anilist.co/forum/thread/{0}";
            public const string ForumCommentUrlTemplate = "http://anilist.co/forum/comment/{0}";
        }

        public static class User
        {
            public const string UserActivityUrlTemplate = "http://anilist.co/activity/{0}";

            public static class Activity
            {
                public static class Types
                {
                    public const string Text = "text";
                    public const string List = "list";
                    public const string Reply = "reply";
                    public const string Message = "message";
                }

                public static class Statues
                {
                    public const string WatchedEpisode = "watched episode";
                    public const string PlansToWatch = "plans to watch";
                }

                public static readonly Regex UserMentionRegEx = new Regex(@"@[^\s]*");

                public static class ImageClickEventTypes
                {
                    public const string Anime = "anime";
                    public const string Manga = "manga";
                    public const string User = "user";
                }
            }
        }
    }
}
