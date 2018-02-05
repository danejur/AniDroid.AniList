using AniDroid.AniList.DataTypes;
using AniDroid.AniList.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Media : BaseAniListObject
    {
        public int IdMal { get; set; }
        public MediaTitle Title { get; set; }
        public string Type { get; set; }
        public string Format { get; set; }
        public string Status { get; set; }
        public string Description { get; set; }
        public FuzzyDate StartDate { get; set; }
        public FuzzyDate EndDate { get; set; }
        public string Season { get; set; }
        public int Episodes { get; set; }
        public int Duration { get; set; }
        public int Chapters { get; set; }
        public int Volumes { get; set; }
        public string CountryOfOrigin { get; set; }
        public bool IsLicensed { get; set; }
        public string Source { get; set; }
        public string Hashtag { get; set; }
        public MediaTrailer Trailer { get; set; }
        public int UpdatedAt { get; set; }
        public AniListImage CoverImage { get; set; }
        public string BannerImage { get; set; }
        public List<string> Genres { get; set; }
        public List<string> Synonyms { get; set; }
        public int AverageScore { get; set; }
        public int MeanScore { get; set; }
        public int Popularity { get; set; }
        public List<MediaTag> Tags { get; set; }
        public MediaRelation Relations { get; set; }
        public Connection<Character.Edge, Character> Characters { get; set; }
        public Connection<Staff.Edge, Staff> Staff { get; set; }
        public Connection<Studio.Edge, Studio> Studios { get; set; }
        public bool IsFavorite { get; set; }
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
            public string Type { get; set; }
            public string Format { get; set; }
            public int Year { get; set; }
            public string Season { get; set; }
            public bool AllTime { get; set; }
            public string Context { get; set; }
        }

        public class MediaList
        {
            public int Id { get; set; }
            public int UserId { get; set; }
            public int MediaId { get; set; }
            public string Status { get; set; }
            public float Score { get; set; }
            public int Progress { get; set; }
            public int ProgressVolumes { get; set; }
            public int Repeat { get; set; }
            public int Priority { get; set; }
            public bool Private { get; set; }
            public string Notes { get; set; }
            public bool HiddenFromStatusLists { get; set; }
            //public List<Tuple<string, bool>> CustomLists { get; set; }    Bug created on github for this
            // advanced scores
            public FuzzyDate StartedAt { get; set; }
            public FuzzyDate CompletedAt { get; set; }
            public int UpdatedAt { get; set; }
            public int CreatedAt { get; set; }
            public Media Media { get; set; }
            public User User { get; set; }
        }

        public class MediaListCollection
        {
            public List<List<MediaList>> StatusLists { get; set; }
            public List<List<MediaList>> CustomLists { get; set; }
            public User User { get; set; }

        }

        public class MediaStats
        {
            public List<AniListScoreDistribution> ScoreDistribution { get; set; }
            public List<AniListStatusDistribution> StatusDistribution { get; set; }
            public List<MediaAiringProgression> AiringProgression { get; set; }
        }

        public class MediaAiringProgression
        {
            public float Episode { get; set; }
            public int Score { get; set; }
            public int Watching { get; set; }
        }

        public class Edge : ConnectionEdge<Media>
        {
            public string RelationType { get; set; }
            public bool IsMainStudio { get; set; }
            public List<Character> Characters { get; set; }
            public string CharacterRole { get; set; }
            public string StaffRole { get; set; }
            public List<Staff> VoiceActors { get; set; }
            public int FavouriteOrder { get; set; }
        }

        #endregion

        #region Enum Classes

        public class MediaType : AniListEnum
        {
            private MediaType(string val, string displayVal) : base(val, displayVal) { }

            public static MediaType Anime => new MediaType("ANIME", "Anime");
            public static MediaType Manga => new MediaType("MANGA", "Manga");
        }

        public class MediaFormat : AniListEnum
        {
            private MediaFormat(string val, string displayVal) : base(val, displayVal) { }

            public static MediaFormat Tv => new MediaFormat("TV", "Tv");
            public static MediaFormat TvShort => new MediaFormat("TV_SHORT", "Tv Short");
            public static MediaFormat Movie => new MediaFormat("MOVIE", "Movie");
            public static MediaFormat Special => new MediaFormat("SPECIAL", "Special");
            public static MediaFormat Ova => new MediaFormat("OVA", "Ova");
            public static MediaFormat Ona => new MediaFormat("ONA", "Ona");
            public static MediaFormat Music => new MediaFormat("MUSIC", "Music");
            public static MediaFormat Manga => new MediaFormat("MANGA", "Manga");
            public static MediaFormat Novel => new MediaFormat("NOVEL", "Novel");
            public static MediaFormat OneShot => new MediaFormat("ONE_SHOT", "One Shot");
        }

        public class MediaStatus : AniListEnum
        {
            private MediaStatus(string val, string displayVal) : base(val, displayVal) { }

            public static MediaStatus Finished => new MediaStatus("FINISHED", "Finished");
            public static MediaStatus Releasing => new MediaStatus("RELEASING", "Releasing");
            public static MediaStatus NotYetReleased => new MediaStatus("NOT_YET_RELEASED", "Not Yet Released");
            public static MediaStatus Cancelled => new MediaStatus("CANCELLED", "Cancelled");
        }

        public class MediaSeason : AniListEnum
        {
            private MediaSeason(string val, string displayVal) : base(val, displayVal) { }

            public static MediaSeason Winter => new MediaSeason("WINTER", "Winter");
            public static MediaSeason Spring => new MediaSeason("SPRING", "Spring");
            public static MediaSeason Summer => new MediaSeason("SUMMER", "Summer");
            public static MediaSeason Fall => new MediaSeason("FALL", "Fall");
        }

        public class MediaSource : AniListEnum
        {
            private MediaSource(string val, string displayVal) : base(val, displayVal) { }

            public static MediaSource Original => new MediaSource("ORIGINAL", "Original");
            public static MediaSource Manga => new MediaSource("MANGA", "Manga");
            public static MediaSource LightNovel => new MediaSource("LIGHT_NOVEL", "Light Novel");
            public static MediaSource VisualNovel => new MediaSource("VISUAL_NOVEL", "Visual Novel");
            public static MediaSource VideoGame => new MediaSource("VIDEO_GAME", "Video Game");
            public static MediaSource Other => new MediaSource("OTHER", "Other");
        }

        public class MediaRelation : AniListEnum
        {
            private MediaRelation(string val, string displayVal) : base(val, displayVal) { }

            public static MediaRelation Adaptation => new MediaRelation("ADAPTATION", "Adaptation");
            public static MediaRelation Prequel => new MediaRelation("PREQUEL", "Prequel");
            public static MediaRelation Sequel => new MediaRelation("SEQUEL", "Sequel");
            public static MediaRelation Parent => new MediaRelation("PARENT", "Parent");
            public static MediaRelation SideStory => new MediaRelation("SIDE_STORY", "Side Story");
            public static MediaRelation Character => new MediaRelation("CHARACTER", "Character");
            public static MediaRelation Summary => new MediaRelation("SUMMARY", "Summary");
            public static MediaRelation Alternative => new MediaRelation("ALTERNATIVE", "Alternative");
            public static MediaRelation SpinOff => new MediaRelation("SPIN_OFF", "Spin-off");
            public static MediaRelation Other => new MediaRelation("OTHER", "Other");
        }

        public class MediaRankType : AniListEnum
        {
            private MediaRankType(string val, string displayVal) : base(val, displayVal) { }

            public static MediaRankType Rated => new MediaRankType("RATED", "Rated");
            public static MediaRankType Popular => new MediaRankType("POPULAR", "Popular");
        }

        public class MediaListStatus : AniListEnum
        {
            private MediaListStatus(string val, string displayVal) : base(val, displayVal) { }

            public static MediaListStatus Current => new MediaListStatus("CURRENT", "Current");
            public static MediaListStatus Planning => new MediaListStatus("PLANNING", "Planning");
            public static MediaListStatus Completed => new MediaListStatus("COMPLETED", "Completed");
            public static MediaListStatus Dropped => new MediaListStatus("DROPPED", "Dropped");
            public static MediaListStatus Paused => new MediaListStatus("PAUSED", "Paused");
            public static MediaListStatus Repeating => new MediaListStatus("REPEATING", "Repeating");
        }

        #endregion

    }
}
