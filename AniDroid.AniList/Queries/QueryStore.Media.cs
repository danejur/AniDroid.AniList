using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
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
    staff {
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
    siteUrl
    isFavourite
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
        /// Parameters: (mediaId: int, page: int, perPage: int)
        /// <para></para>
        /// Returns: Media with PagedData of Characters with Staff
        /// </summary>
        public static string GetMediaCharacters => @"
query ($mediaId: Int!, $page: Int, $perPage: Int) {
  Data: Media(id: $mediaId) {
    id
    type
    characters(page: $page, perPage: $perPage) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
        hasNextPage
      }
      edges {
        role
        voiceActors {
          id
          name {
            first
            last
            native
          }
          image {
            large
          }
          language
          isFavourite
        }
        node {
          id
          name {
            first
            last
            native
            alternative
          }
          image {
            large
          }
          siteUrl
          isFavourite
        }
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (mediaId: int, page: int, perPage: int)
        /// <para></para>
        /// Returns: Media with PagedData of Staff sorted by language
        /// </summary>
        public static string GetMediaStaff => @"
query ($mediaId: Int!, $page: Int, $perPage: Int) {
  Data: Media(id: $mediaId) {
    id
    type
    staff(page: $page, perPage: $perPage, sort: LANGUAGE) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
        hasNextPage
      }
      edges {
        role
        node {
          id
          name {
            first
            last
            native
          }
          image {
            large
          }
          siteUrl
          language
          isFavourite
        }
      }
    }
  }
}
";
    }
}
