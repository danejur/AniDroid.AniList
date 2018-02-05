using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        public const string GetMediaByIdAndType = @"query ($id: Int!, $type: MediaType) {
  Data: Media(id: $id, type: $type) {
    id
    title {
      romaji
      english
      native
      userPreferred
    }
    startDate {
      year
      month
      day
    }
    endDate {
      year
      month
      day
    }
    coverImage {
      large
      medium
    }
    characters{
        nodes {
          id
          description
          name {
            first
            last
            native
            alternative
          }
        }
    }
    bannerImage
    format
    type
    status
    episodes
    chapters
    volumes
    season
    description
    averageScore
    meanScore
    genres
    synonyms
    nextAiringEpisode {
      airingAt
      timeUntilAiring
      episode
    }
  }
}";
    }
}
