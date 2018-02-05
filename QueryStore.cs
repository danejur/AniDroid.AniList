using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList
{
    internal class QueryStore
    {
        public const string GetSeriesByIdAndType = @"query ($id: Int!, $type: MediaType) {
  Media(id: $id, type: $type) {
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

        public const string GetUserByName = "query ($name: String) { User (name: $name) { id name about(asHtml: true) avatar { large medium } bannerImage isFollowing options { titleLanguage displayAdultContent } mediaListOptions { 	scoreFormat rowOrder useLegacyLists animeList { splitCompletedSectionByFormat advancedScoringEnabled sectionOrder } mangaList { splitCompletedSectionByFormat advancedScoringEnabled sectionOrder } } donatorTier unreadNotificationCount siteUrl updatedAt } }";
    }
}
