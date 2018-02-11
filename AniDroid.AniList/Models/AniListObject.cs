using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public abstract class AniListObject
    {
        public int Id { get; set; }

        public class PageInfo
        {
            public int Total { get; set; }
            public int PerPage { get; set; }
            public int CurrentPage { get; set; }
            public int LastPage { get; set; }
            public bool HasNextPage { get; set; }
        }

        public class PagedData<T>
        {
            public PageInfo PageInfo { get; set; }
            public T Data { get; set; }
        }

        public class AniListImage
        {
            public string Large { get; set; }
            public string Medium { get; set; }
        }

        public class AniListName
        {
            public string First { get; set; }
            public string Last { get; set; }
            public string Native { get; set; }
            public List<string> Alternative { get; set; }

            public string GetFormattedName(bool nativeLineBreak = false)
            {
                var retName = "(Name Unknown)";

                if (!string.IsNullOrWhiteSpace(First))
                {
                    retName = First;
                }
                if (!string.IsNullOrWhiteSpace(Last))
                {
                    retName += $" {Last}";
                }
                if (!string.IsNullOrWhiteSpace(Native))
                {
                    retName += $"{(nativeLineBreak ? "\n" :" ")}({Native})";
                }

                return retName;
            }
        }

        public class AniListStatusDistribution
        {
            public string Status { get; set; }
            public int Amount { get; set; }
        }

        public class AniListScoreDistribution
        {
            public int Score { get; set; }
            public int Amount { get; set; }
        }

        public class Connection<EdgeType, NodeType> where EdgeType : ConnectionEdge<NodeType> where NodeType : AniListObject
        {
            public List<EdgeType> Edges { get; set; }
            public List<NodeType> Nodes { get; set; }
            public PageInfo PageInfo { get; set; }
        }

        public abstract class ConnectionEdge<NodeType> where NodeType : AniListObject
        {
            public int Id { get; set; }
            public NodeType Node { get; set; }
        }

        public DateTimeOffset GetDateTimeOffset(int sec)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sec);
        }

        public string GetAgeString(int sec)
        {
            var ageSeconds = DateTimeOffset.Now.ToUnixTimeSeconds() - sec;

            if (ageSeconds < 60)
            {
                return $"{ageSeconds} second{(ageSeconds != 1 ? "s" : "")} ago";
            }
            else if (ageSeconds < 3600)
            {
                var ageMinutes = ageSeconds / 60;
                return $"{ageMinutes} minute{(ageMinutes != 1 ? "s" : "")} ago";
            }
            else if (ageSeconds < 86400)
            {
                var ageHours = ageSeconds / 3600;
                return $"{ageHours} hour{(ageHours != 1 ? "s" : "")} ago";
            }
            else
            {
                var ageDays = ageSeconds / 86400;
                return $"{ageDays} day{(ageDays != 1 ? "s" : "")} ago";
            }
        }

        #region Enum Classes

        public sealed class AniListTitleLanguage : AniListEnum
        {
            private AniListTitleLanguage(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static AniListTitleLanguage Romaji => new AniListTitleLanguage("ROMAJI", "Romaji", 0);
            public static AniListTitleLanguage English => new AniListTitleLanguage("ENGLISH", "English", 1);
            public static AniListTitleLanguage Native => new AniListTitleLanguage("NATIVE", "Native", 2);
            public static AniListTitleLanguage RomajiStylised => new AniListTitleLanguage("ROMAJI_STYLISED", "Romaji Stylised", 3);
            public static AniListTitleLanguage EnglishStylised => new AniListTitleLanguage("ENGLISH_STYLISED", "English Stylised", 4);
            public static AniListTitleLanguage NativeStylised => new AniListTitleLanguage("NATIVE_STYLISED", "Native Stylised", 5);
        }

        public sealed class LikeableType : AniListEnum
        {
            private LikeableType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static LikeableType Thread => new LikeableType("THREAD", "Thread", 0);
            public static LikeableType ThreadComment => new LikeableType("THREAD_COMMENT", "Thread Comment", 1);
            public static LikeableType Activity => new LikeableType("ACTIVITY", "Activity", 2);
            public static LikeableType ActivityReply => new LikeableType("ACTIVITY_REPLY", "Activity Reply", 3);
        }

        #endregion

    }
}
