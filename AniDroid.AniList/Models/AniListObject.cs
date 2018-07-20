using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.DataTypes;
using AniDroid.AniList.Interfaces;
using Newtonsoft.Json;

namespace AniDroid.AniList.Models
{
    public abstract class AniListObject
    {
        public int Id { get; set; }

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

            public string GetFormattedName(bool nativeLineBreak = false) =>
                $"{$"{First} {Last}".Trim()}{(string.IsNullOrWhiteSpace(Native) ? "" : ($"{(nativeLineBreak ? "\n" : " ")}({Native})"))}";
        }

        public class AniListStatusDistribution
        {
            public Media.MediaListStatus Status { get; set; }
            public int Amount { get; set; }
        }

        public class AniListScoreDistribution
        {
            public int Score { get; set; }
            public int Amount { get; set; }
        }

        public class Connection<TEdgeType, TNodeType> : IPagedData<TEdgeType> where TEdgeType : ConnectionEdge<TNodeType> where TNodeType : AniListObject
        {
            [JsonProperty("Edges")]
            public ICollection<TEdgeType> Data { get; set; }
            public ICollection<TNodeType> Nodes { get; set; }
            public PageInfo PageInfo { get; set; }
        }

        public abstract class ConnectionEdge<TNodeType> where TNodeType : AniListObject
        {
            public int Id { get; set; }
            public TNodeType Node { get; set; }
        }

        public DateTimeOffset GetDateTimeOffset(int sec)
        {
            return DateTimeOffset.FromUnixTimeSeconds(sec);
        }

        public string GetDurationString(long seconds, int precision = 0)
        {
            if (seconds < 60)
            {
                return $"{seconds} second{(seconds != 1 ? "s" : "")}";
            }

            if (seconds < 3600)
            {
                var ageMinutes = decimal.Round((decimal)seconds / 60, precision, MidpointRounding.AwayFromZero);
                return $"{ageMinutes} minute{(ageMinutes != 1 ? "s" : "")}";
            }

            if (seconds < 86400)
            {
                var ageHours = decimal.Round((decimal)seconds / 3600, precision, MidpointRounding.AwayFromZero);
                return $"{ageHours} hour{(ageHours != 1 ? "s" : "")}";
            }

            var ageDays = decimal.Round((decimal)seconds / 86400, precision, MidpointRounding.AwayFromZero);
            return $"{ageDays} day{(ageDays != 1 ? "s" : "")}";
        }

        public string GetAgeString(long seconds)
        {
            return $"{GetDurationString(DateTimeOffset.Now.ToUnixTimeSeconds() - seconds)} ago";
        }

        public string GetFormattedDateString(long sec)
        {
            var date = DateTimeOffset.FromUnixTimeSeconds(sec);
            return date.ToString("MMMM dd, yyyy");
        }

        #region Enum Classes

        public sealed class AniListTitleLanguage : AniListEnum
        {
            private AniListTitleLanguage(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static AniListTitleLanguage Romaji { get; } = new AniListTitleLanguage("ROMAJI", "Romaji", 0);
            public static AniListTitleLanguage English { get; } = new AniListTitleLanguage("ENGLISH", "English", 1);
            public static AniListTitleLanguage Native { get; } = new AniListTitleLanguage("NATIVE", "Native", 2);
            public static AniListTitleLanguage RomajiStylised { get; } = new AniListTitleLanguage("ROMAJI_STYLISED", "Romaji Stylised", 3);
            public static AniListTitleLanguage EnglishStylised { get; } = new AniListTitleLanguage("ENGLISH_STYLISED", "English Stylised", 4);
            public static AniListTitleLanguage NativeStylised { get; } = new AniListTitleLanguage("NATIVE_STYLISED", "Native Stylised", 5);
        }

        public sealed class LikeableType : AniListEnum
        {
            private LikeableType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static LikeableType Thread { get; } = new LikeableType("THREAD", "Thread", 0);
            public static LikeableType ThreadComment { get; } = new LikeableType("THREAD_COMMENT", "Thread Comment", 1);
            public static LikeableType Activity { get; } = new LikeableType("ACTIVITY", "Activity", 2);
            public static LikeableType ActivityReply { get; } = new LikeableType("ACTIVITY_REPLY", "Activity Reply", 3);
        }

        #endregion

    }
}
