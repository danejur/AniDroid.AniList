using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.Models;

namespace AniDroid.AniList.Dto
{
    public class BrowseMediaDto
    {
        // TODO: add rest of properties from query

        public ICollection<Media.MediaSort> Sort { get; set; }
        public Media.MediaType Type { get; set; }
        public Media.MediaSeason Season { get; set; }
        public Media.MediaStatus Status { get; set; } = null;
        public int? SeasonYear { get; set; }
        public Media.MediaFormat Format { get; set; }
        public int? Year { get; set; }
        public string YearLike => Year.HasValue ? $"{Year}%" : null;
        public int? PopularityGreaterThan { get; set; }
        public int? AverageGreaterThan { get; set; }
        public int? EpisodesGreaterThan { get; set; }
        public int? EpisodesLessThan { get; set; }
        public Media.MediaCountry Country { get; set; }
        public Media.MediaSource Source { get; set; }

        public ICollection<string> IncludedGenres { get; set; }
        public ICollection<string> ExcludedGenres { get; set; }
        public ICollection<string> IncludedTags { get; set; }
        public ICollection<string> ExcludedTags { get; set; }

        public ICollection<string> LicensedBy { get; set; }
    }
}
