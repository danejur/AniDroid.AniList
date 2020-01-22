using System;
using AniDroid.AniList.DataTypes;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using Newtonsoft.Json.Linq;

namespace AniDroid.AniList.Models
{
    public class Media : AniListObject
    {
        public int IdMal { get; set; }
        public MediaTitle Title { get; set; }
        public MediaType Type { get; set; }
        public MediaFormat Format { get; set; }
        public MediaStatus Status { get; set; }
        public string Description { get; set; }
        public FuzzyDate StartDate { get; set; }
        public FuzzyDate EndDate { get; set; }
        public MediaSeason Season { get; set; }
        public int? Episodes { get; set; }
        public int? Duration { get; set; }
        public int? Chapters { get; set; }
        public int? Volumes { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool IsLicensed { get; set; }
        public MediaSource Source { get; set; }
        public string Hashtag { get; set; }
        public MediaTrailer Trailer { get; set; }
        public int UpdatedAt { get; set; }
        public MediaCoverImage CoverImage { get; set; }
        public string BannerImage { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Synonyms { get; set; }
        public int AverageScore { get; set; }
        public int MeanScore { get; set; }
        public int Popularity { get; set; }
        public List<MediaTag> Tags { get; set; }
        public Connection<Edge, Media> Relations { get; set; }
        public Connection<Character.Edge, Character> Characters { get; set; }
        public Connection<Staff.Edge, Staff> Staff { get; set; }
        public Connection<Studio.Edge, Studio> Studios { get; set; }
        public Connection<Recommendation.Edge, Recommendation> Recommendations { get; set; }
        public bool IsFavourite { get; set; }
        public bool IsAdult { get; set; }
        public AiringSchedule NextAiringEpisode { get; set; }
        public Connection<AiringSchedule.Edge, AiringSchedule> AiringSchedule { get; set; }
        public List<MediaExternalLink> ExternalLinks { get; set; }
        public List<MediaStreamingEpisode> StreamingEpisodes { get; set; }
        public List<MediaRank> Rankings { get; set; }
        public MediaList MediaListEntry { get; set; }
        public Connection<Review.Edge, Review> Reviews { get; set; }
        public MediaStats Stats { get; set; }
        public string SiteUrl { get; set; }
        public bool AutoCreateForumThread { get; set; }
        public int Trending { get; set; }

        public string GetFormattedDateRangeString()
        {
            var retString = "";

            if (StartDate.IsValid() && EndDate.IsValid())
            {
                retString = StartDate.Equals(EndDate) ? StartDate.GetFuzzyDateString() : $"{StartDate.GetFuzzyDateString()} to {EndDate.GetFuzzyDateString()}";
            }
            else if (StartDate.IsValid())
            {
                retString = $"{(Status == MediaStatus.NotYetReleased ? "Starts" : "Started")} {StartDate.GetFuzzyDateString()}";
            }
            else if (EndDate.IsValid())
            {
                retString = $"{(Status == MediaStatus.Finished || Status == MediaStatus.Cancelled ? "Ended" : "Ending")} {EndDate.GetFuzzyDateString()}";
            }

            return retString;
        }

        #region Internal Classes

        public class MediaTitle
        {
            public string Romaji { get; set; }
            public string English { get; set; }
            public string Native { get; set; }
            public string UserPreferred { get; set; }
        }

        public class MediaTrailer
        {
            public string Id { get; set; }
            public string Site { get; set; }
        }

        public class MediaTag
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Description { get; set; }
            public string Category { get; set; }
            public int Rank { get; set; }
            public bool IsGeneralSpoiler { get; set; }
            public bool IsMediaSpoiler { get; set; }
            public bool IsAdult { get; set; }
        }

        public class MediaExternalLink
        {
            public int Id { get; set; }
            public string Url { get; set; }
            public string Site { get; set; }
        }

        public class MediaStreamingEpisode
        {
            public string Title { get; set; }
            public string Thumbnail { get; set; }
            public string Url { get; set; }
            public string Site { get; set; }
        }

        public class MediaRank
        {
            public int Id { get; set; }
            public int Rank { get; set; }
            public MediaRankType Type { get; set; }
            public MediaFormat Format { get; set; }
            public int? Year { get; set; }
            public MediaSeason Season { get; set; }
            public bool AllTime { get; set; }
            public string Context { get; set; }

            public string GetFormattedRankString()
            {
                return $"#{Rank} {CultureInfo.CurrentCulture.TextInfo.ToTitleCase(Context)}" +
                       (Season != null ? $" {Season.DisplayValue}" : "") +
                       (Year.HasValue ? $" {Year}" : "") +
                       (Format != null ? $" ({Format.DisplayValue})" : "");
            }
        }

        public class MediaList : AniListObject
        {
            public int UserId { get; set; }
            public int MediaId { get; set; }
            public MediaListStatus Status { get; set; }
            public float Score { get; set; }
            public int? Progress { get; set; }
            public int? ProgressVolumes { get; set; }
            public int? Repeat { get; set; }
            public int Priority { get; set; }
            public bool Private { get; set; }
            public string Notes { get; set; }
            public bool HiddenFromStatusLists { get; set; }
            public List<MediaCustomList> CustomLists { get; set; }
            // TODO: work-around. see about getting this changed to an array
            public dynamic AdvancedScores { get; set; }
            public FuzzyDate StartedAt { get; set; }
            public FuzzyDate CompletedAt { get; set; }
            public int UpdatedAt { get; set; }
            public int CreatedAt { get; set; }
            public Media Media { get; set; }
            public User User { get; set; }

            public string GetScoreString(User.ScoreFormat scoreFormat)
            {
                if (scoreFormat == User.ScoreFormat.ThreeSmileys)
                {
                    return new[] {"🤔 (no score)", "🙁", "😐", "🙂"}[Math.Min((int)Score, 3)];
                }

                if (Score == 0)
                {
                    return "No score given";
                }

                if (scoreFormat == User.ScoreFormat.TenDecimal)
                {
                    return $"{Score:#.#} / 10";
                }

                if (scoreFormat == User.ScoreFormat.FiveStars)
                {
                    return string.Concat(Enumerable.Repeat("★", (int)Score));
                }

                return scoreFormat == User.ScoreFormat.Ten ? $"{Score:#} / 10" : $"{Score:#} / 100";
            }

            public string GetFormattedProgressString(MediaType type, int? maxProgress)
            {
                var retStr = "";

                if (Status == MediaListStatus.Completed)
                {
                    retStr = CompletedAt?.IsValid() == true
                        ? $"Completed on:  {CompletedAt.GetFuzzyDateString()}"
                        : "Completed on unknown date";
                }
                else if (Status == MediaListStatus.Planning)
                {
                    retStr = $"Added to lists:  {GetFormattedDateString(CreatedAt)}";
                }
                else if (Status == MediaListStatus.Current || Status == MediaListStatus.Dropped || Status == MediaListStatus.Paused || Status == MediaListStatus.Repeating)
                {
                    var progressType = "";

                    if (type == MediaType.Anime)
                    {
                        progressType = $"episode{(Progress == 1 ? "" : "s")}";
                    }
                    else if (type == MediaType.Manga)
                    {
                        progressType = $"chapter{(Progress == 1 ? "" : "s")}";
                    }

                    retStr =
                        $"Progress:  {Progress}{(maxProgress.HasValue ? $" / {maxProgress} " : " ")}{progressType}";
                }

                if ((Repeat ?? 0) > 0)
                {
                    retStr += $"  (Repeats:  {Repeat})";
                }

                return retStr;
            }
        }

        public class MediaListGroup
        {
            public string Name { get; set; }
            public bool IsCustomList { get; set; }
            public bool IsSplitCompletedList { get; set; }
            public MediaListStatus Status { get; set; }
            public List<MediaList> Entries { get; set; }
        }

        public class MediaListCollection
        {
            public List<MediaListGroup> Lists { get; set; }
            public User User { get; set; }

        }

        public class MediaStats
        {
            public List<AniListScoreDistribution> ScoreDistribution { get; set; }
            public List<AniListStatusDistribution> StatusDistribution { get; set; }
            public List<MediaAiringProgression> AiringProgression { get; set; }

            public bool AreStatsValid()
            {
                return ScoreDistribution?.Count(x => x.Amount > 0) >= 3 || AiringProgression?.Count >= 3 ||
                       StatusDistribution?.Any(x => x.Amount >= 3) == true;
            }
        }

        public class MediaAiringProgression
        {
            public float Episode { get; set; }
            public float Score { get; set; }
            public int Watching { get; set; }
        }

        public class Edge : ConnectionEdge<Media>
        {
            public MediaRelation RelationType { get; set; }
            public bool IsMainStudio { get; set; }
            public List<Character> Characters { get; set; }
            public Character.CharacterRole CharacterRole { get; set; }
            public string StaffRole { get; set; }
            public List<Staff> VoiceActors { get; set; }
            public int FavouriteOrder { get; set; }
        }

        public class MediaCustomList
        {
            public string Name { get; set; }
            public bool Enabled { get; set; }
        }

        public class MediaCoverImage : AniListImage
        {
            public string ExtraLarge { get; set; }
            public string Color { get; set; }
        }

        #endregion

        #region Enum Classes

        /// <summary>
        /// Describes the type of Media (e.g. Anime or Manga)
        /// </summary>
        public sealed class MediaType : AniListEnum
        {
            private MediaType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaType Anime { get; } = new MediaType("ANIME", "Anime", 0);
            public static MediaType Manga { get; } = new MediaType("MANGA", "Manga", 1);
        }

        /// <summary>
        /// Describes the format of Media (e.g. Tv, Manga, etc.)
        /// </summary>
        public sealed class MediaFormat : AniListEnum
        {
            public MediaType MediaType { get; private set; }

            private MediaFormat(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaFormat Tv { get; } = new MediaFormat("TV", "TV", 0) { MediaType = MediaType.Anime };
            public static MediaFormat TvShort { get; } = new MediaFormat("TV_SHORT", "TV Short", 1) { MediaType = MediaType.Anime };
            public static MediaFormat Movie { get; } = new MediaFormat("MOVIE", "Movie", 2) { MediaType = MediaType.Anime };
            public static MediaFormat Special { get; } = new MediaFormat("SPECIAL", "Special", 3) { MediaType = MediaType.Anime };
            public static MediaFormat Ova { get; } = new MediaFormat("OVA", "OVA", 4) { MediaType = MediaType.Anime };
            public static MediaFormat Ona { get; } = new MediaFormat("ONA", "ONA", 5) { MediaType = MediaType.Anime };
            public static MediaFormat Music { get; } = new MediaFormat("MUSIC", "Music", 6) { MediaType = MediaType.Anime };
            public static MediaFormat Manga { get; } = new MediaFormat("MANGA", "Manga", 7) { MediaType = MediaType.Manga };
            public static MediaFormat Novel { get; } = new MediaFormat("NOVEL", "Novel", 8) { MediaType = MediaType.Manga };
            public static MediaFormat OneShot { get; } = new MediaFormat("ONE_SHOT", "One Shot", 9) { MediaType = MediaType.Manga };
        }

        /// <summary>
        /// Describes the satus of the Media (e.g. Finished, Releasing, etc.)
        /// </summary>
        public sealed class MediaStatus : AniListEnum
        {
            private MediaStatus(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaStatus Finished { get; } = new MediaStatus("FINISHED", "Finished", 0);
            public static MediaStatus Releasing { get; } = new MediaStatus("RELEASING", "Releasing", 1);
            public static MediaStatus NotYetReleased { get; } = new MediaStatus("NOT_YET_RELEASED", "Not Yet Released", 2);
            public static MediaStatus Cancelled { get; } = new MediaStatus("CANCELLED", "Cancelled", 3);
        }

        /// <summary>
        /// Describes the season of the Media (e.g. Winter, Summer, etc.)
        /// </summary>
        public sealed class MediaSeason : AniListEnum
        {
            private MediaSeason(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaSeason Winter { get; } = new MediaSeason("WINTER", "Winter", 0);
            public static MediaSeason Spring { get; } = new MediaSeason("SPRING", "Spring", 1);
            public static MediaSeason Summer { get; } = new MediaSeason("SUMMER", "Summer", 2);
            public static MediaSeason Fall { get; } = new MediaSeason("FALL", "Fall", 3);

            public static MediaSeason GetFromDate(DateTime date)
            {
                return GetEnum<MediaSeason>((date.Month - 1) / 3);
            }
        }

        /// <summary>
        /// Describes the source of the Media (e.g. Original, Light Novel, etc.)
        /// </summary>
        public sealed class MediaSource : AniListEnum
        {
            private MediaSource(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaSource Original { get; } = new MediaSource("ORIGINAL", "Original", 0);
            public static MediaSource Manga { get; } = new MediaSource("MANGA", "Manga", 1);
            public static MediaSource LightNovel { get; } = new MediaSource("LIGHT_NOVEL", "Light Novel", 2);
            public static MediaSource VisualNovel { get; } = new MediaSource("VISUAL_NOVEL", "Visual Novel", 3);
            public static MediaSource VideoGame { get; } = new MediaSource("VIDEO_GAME", "Video Game", 4);
            public static MediaSource Other { get; } = new MediaSource("OTHER", "Other", 5);
        }

        /// <summary>
        /// Describes the relation of the Media to a related Media (e.g. Adaptation, Sequel, etc.)
        /// </summary>
        public sealed class MediaRelation : AniListEnum
        {
            private MediaRelation(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaRelation Adaptation { get; } = new MediaRelation("ADAPTATION", "Adaptation", 0);
            public static MediaRelation Prequel { get; } = new MediaRelation("PREQUEL", "Prequel", 1);
            public static MediaRelation Sequel { get; } = new MediaRelation("SEQUEL", "Sequel", 2);
            public static MediaRelation Parent { get; } = new MediaRelation("PARENT", "Parent", 3);
            public static MediaRelation SideStory { get; } = new MediaRelation("SIDE_STORY", "Side Story", 4);
            public static MediaRelation Character { get; } = new MediaRelation("CHARACTER", "Character", 5);
            public static MediaRelation Summary { get; } = new MediaRelation("SUMMARY", "Summary", 6);
            public static MediaRelation Alternative { get; } = new MediaRelation("ALTERNATIVE", "Alternative", 7);
            public static MediaRelation SpinOff { get; } = new MediaRelation("SPIN_OFF", "Spin-off", 8);
            public static MediaRelation Other { get; } = new MediaRelation("OTHER", "Other", 9);
        }

        /// <summary>
        /// Describes the ranking type of the Media (e.g. Rated, Popular)
        /// </summary>
        public sealed class MediaRankType : AniListEnum
        {
            private MediaRankType(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaRankType Rated { get; } = new MediaRankType("RATED", "Rated", 0);
            public static MediaRankType Popular { get; } = new MediaRankType("POPULAR", "Popular", 1);
        }

        /// <summary>
        /// Describes the list status of the Media (e.g. Current, Completed, etc.)
        /// </summary>
        public sealed class MediaListStatus : AniListEnum
        {
            private MediaListStatus(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaListStatus Current { get; } = new MediaListStatus("CURRENT", "Current", 0);
            public static MediaListStatus Planning { get; } = new MediaListStatus("PLANNING", "Planning", 1);
            public static MediaListStatus Completed { get; } = new MediaListStatus("COMPLETED", "Completed", 2);
            public static MediaListStatus Dropped { get; } = new MediaListStatus("DROPPED", "Dropped", 3);
            public static MediaListStatus Paused { get; } = new MediaListStatus("PAUSED", "Paused", 4);
            public static MediaListStatus Repeating { get; } = new MediaListStatus("REPEATING", "Repeating", 5);
        }

        /// <summary>
        /// Describes the sort method used for browsing (e.g. Score, Id, etc.)
        /// </summary>
        public sealed class MediaSort : AniListEnum
        {
            private MediaSort(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaSort Id { get; } = new MediaSort("ID", "Id", 0);
            public static MediaSort IdDesc { get; } = new MediaSort("ID_DESC", "Id (Desc)", 1);
            public static MediaSort TitleRomaji { get; } = new MediaSort("TITLE_ROMAJI", "Title - Romaji", 2);
            public static MediaSort TitleRomajiDesc { get; } = new MediaSort("TITLE_ROMAJI_DESC", "Title - Romaji (Desc)", 3);
            public static MediaSort TitleEnglish { get; } = new MediaSort("TITLE_ENGLISH", "Title - English", 4);
            public static MediaSort TitleEnglishDesc { get; } = new MediaSort("TITLE_ENGLISH_DESC", "Title - English (Desc)", 5);
            public static MediaSort TitleNative { get; } = new MediaSort("TITLE_NATIVE", "Title - Native", 6);
            public static MediaSort TitleNativeDesc { get; } = new MediaSort("TITLE_NATIVE_DESC", "Title - Native (Desc)", 7);
            public static MediaSort Type { get; } = new MediaSort("TYPE", "Type", 8);
            public static MediaSort TypeDesc { get; } = new MediaSort("TYPE_DESC", "Type (Desc)", 9);
            public static MediaSort Format { get; } = new MediaSort("FORMAT", "Format", 10);
            public static MediaSort FormatDesc { get; } = new MediaSort("FORMAT_DESC", "Format (Desc)", 11);
            public static MediaSort StartDate { get; } = new MediaSort("START_DATE", "Start Date", 12);
            public static MediaSort StartDateDesc { get; } = new MediaSort("START_DATE_DESC", "Start Date (Desc)", 13);
            public static MediaSort EndDate { get; } = new MediaSort("END_DATE", "End Date", 14);
            public static MediaSort EndDateDesc { get; } = new MediaSort("END_DATE_DESC", "End Date (Desc)", 15);
            public static MediaSort Score { get; } = new MediaSort("SCORE", "Score", 16);
            public static MediaSort ScoreDesc { get; } = new MediaSort("SCORE_DESC", "Score (Desc)", 17);
            public static MediaSort Popularity { get; } = new MediaSort("POPULARITY", "Popularity", 18);
            public static MediaSort PopularityDesc { get; } = new MediaSort("POPULARITY_DESC", "Popularity (Desc)", 19);
            public static MediaSort Trending { get; } = new MediaSort("TRENDING", "Trending", 20);
            public static MediaSort TrendingDesc { get; } = new MediaSort("TRENDING_DESC", "Trending (Desc)", 21);
        }

        /// <summary>
        /// Describes the country of origin of the Media (e.g. Japan, Korea, China)
        /// </summary>
        public sealed class MediaCountry : AniListEnum
        {
            public MediaCountry(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaCountry Japan { get; } = new MediaCountry("JP", "Japan", 0);
            public static MediaCountry Korea { get; } = new MediaCountry("KR", "Korea", 1);
            public static MediaCountry China { get; } = new MediaCountry("CN", "China", 2);
        }

        /// <summary>
        /// Describes the license holder of the Media (e.g. Netflix, Amazon, etc.)
        /// </summary>
        public sealed class MediaLicensee : AniListEnum
        {
            public MediaLicensee(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static MediaLicensee Crunchyroll { get; } = new MediaLicensee("Cruncyroll", "Crunchyroll", 0);
            public static MediaLicensee Funimation { get; } = new MediaLicensee("Funimation", "Funimation", 1);
            public static MediaLicensee Netflix { get; } = new MediaLicensee("Netflix", "Netflix", 2);
            public static MediaLicensee Amazon { get; } = new MediaLicensee("Amazon", "Amazon", 3);
            public static MediaLicensee Hidive { get; } = new MediaLicensee("Hidive", "Hidive", 4);
            public static MediaLicensee Hulu { get; } = new MediaLicensee("Hulu", "Hulu", 5);
            public static MediaLicensee Animelab { get; } = new MediaLicensee("Animelab", "Animelab", 6);
        }

        #endregion

    }
}
