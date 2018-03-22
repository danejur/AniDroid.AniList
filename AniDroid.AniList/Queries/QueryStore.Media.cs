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
      trending
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
        /// Parameters: (page: int, count: int, type?: MediaType, season?: MediaSeason, seasonYear?: int, format?: MediaFormat, status?: MediaStatus, isAdult?: bool, includedGenres?: [string], excludedGenres?: [string], includedTags?: [string], excludedTags?: [string], yearLike: string)
        /// <para></para>
        /// Returns: PagedData of Media
        /// </summary>
        public static string BrowseMedia => @"
query ($page: Int, $count: Int, $sort: [MediaSort], $type: MediaType, $season: MediaSeason, $seasonYear: Int, $format: MediaFormat, $status: MediaStatus, $isAdult: Boolean, $includedGenres: [String], $excludedGenres: [String], $includedTags: [String], $excludedTags: [String], $yearLike: String, $popularityGreaterThan: Int, $averageGreaterThan: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: media(sort: $sort, type: $type, season: $season, seasonYear: $seasonYear, format: $format, status: $status, isAdult: $isAdult, genre_in: $includedGenres, genre_not_in: $excludedGenres, tag_in: $includedTags, tag_not_in: $excludedTags, startDate_like: $yearLike, popularity_greater: $popularityGreaterThan, averageScore_greater: $averageGreaterThan) {
      id
      type
      format
      popularity
      averageScore
      isFavourite
      isAdult
      trending
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
    relations {
      edges {
        relationType
        node {
          id
          isAdult
          title {
            userPreferred
          }
          format
          type
          coverImage {
            large
          }
        }
      }
    }
    studios {
      edges {
        node {
          id
          name
          siteUrl
          isFavourite
        }
        isMain
      }
    }
    bannerImage
    duration
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
    popularity
    genres
    siteUrl
    isFavourite
    synonyms
    trending
    rankings {
      id
      rank
      type
      format
      year
      season
      allTime
      context
    }
    mediaListEntry {
      status
    }
    stats {
      scoreDistribution {
        score
        amount
      }
      statusDistribution {
        status
        amount
      }
      airingProgression {
        episode
        score
        watching
      }
    }
    tags {
      id
      name
      description
      isGeneralSpoiler
      isMediaSpoiler
    }
    nextAiringEpisode {
      airingAt
      timeUntilAiring
      episode
    }
    streamingEpisodes {
      title
      thumbnail
      url
      site
    }
    mediaListEntry {
      user {
        mediaListOptions {
          scoreFormat
          animeList {
            advancedScoring
            advancedScoringEnabled
            customLists
          }
          mangaList {
            advancedScoring
            advancedScoringEnabled
            customLists
          }
        }
      }
      status
      score
      progress
      progressVolumes
      repeat
      priority
      private
      notes
      hiddenFromStatusLists
      customLists
      startedAt {
        year
        month
        day
      }
      completedAt {
        year
        month
        day
      }
      updatedAt
      createdAt
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

        /// <summary>
        /// Parameters: (mediaId: int, status: MediaListStatus, score?: float, progress?: int, progressVolumes?: int, repeat?: int, notes: string, private: bool)
        /// <para></para>
        /// Returns: MediaList
        /// </summary>
        public static string UpdateMediaList => @"
mutation ($mediaId: Int, $status: MediaListStatus, $score: Float, $progress: Int, $progressVolumes: Int, $repeat: Int, $notes: String, $private: Boolean) {
  Data: SaveMediaListEntry(mediaId: $mediaId, status: $status, score: $score, progress: $progress, progressVolumes: $progressVolumes, repeat: $repeat, notes: $notes, private: $private) {
    id
    userId
    mediaId
    status
    score
    progress
    progressVolumes
    repeat
    priority
    private
    notes
    hiddenFromStatusLists
    customLists
    startedAt {
      year
      month
      day
    }
    completedAt {
      year
      month
      day
    }
    updatedAt
    createdAt
  }
}
";
    }
}
