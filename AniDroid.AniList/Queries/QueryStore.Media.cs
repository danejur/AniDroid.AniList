using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        /// <summary>
        /// Parameters: (mediaId: int)
        /// <para></para>
        /// Returns: Media
        /// </summary>
        public static string GetMediaById => @"
query ($mediaId: Int!) {
  Data: Media(id: $mediaId) {
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
    characters {
      pageInfo {
        total
        perPage
        hasNextPage
        currentPage
        lastPage
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
}
";

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
