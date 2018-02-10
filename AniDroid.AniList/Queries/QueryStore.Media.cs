using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        /// <summary>
        /// Parameters: (id: int, type: MediaType)
        /// <para></para>
        /// Returns: Media
        /// </summary>
        public static string GetMediaByIdAndType => @"query ($id: Int!, $type: MediaType) {
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

        /// <summary>
        /// Parameters: (queryText: string, page: int, count: int, type?: MediaType)
        /// <para></para>
        /// Returns: PagedData of Media
        /// </summary>
        public static string SearchMedia => @"
query ($queryText: String, $page:Int, $count:Int, $type:MediaType) {
  Data: Page(page:$page, perPage:$count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: media(search: $queryText, type: $type) {
      id
      type
      format
      popularity
      averageScore
      isFavourite
      isAdult
      title {
        userPreferred
      }
      coverImage {
        large
      }
    }
  }
}
";
    }
}
