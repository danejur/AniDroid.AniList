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

        #region Enum Classes

        public class AniListTitleLanguage : AniListEnum
        {
            private AniListTitleLanguage(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static AniListTitleLanguage Romaji => new AniListTitleLanguage("ROMAJI", "Romaji", 0);
            public static AniListTitleLanguage English => new AniListTitleLanguage("ENGLISH", "English", 1);
            public static AniListTitleLanguage Native => new AniListTitleLanguage("NATIVE", "Native", 2);
            public static AniListTitleLanguage RomajiStylised => new AniListTitleLanguage("ROMAJI_STYLISED", "Romaji Stylised", 3);
            public static AniListTitleLanguage EnglishStylised => new AniListTitleLanguage("ENGLISH_STYLISED", "English Stylised", 4);
            public static AniListTitleLanguage NativeStylised => new AniListTitleLanguage("NATIVE_STYLISED", "Native Stylised", 5);
        }

        #endregion

    }
}
