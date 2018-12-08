using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        /// <summary>
        /// Returns: User
        /// <para></para>
        /// (Must be authorized)
        /// </summary>
        public static string GetCurrentUser => @"
{
  Data: Viewer {
    id
    name
    bannerImage
    avatar {
      large
    }
    options {
      titleLanguage
      displayAdultContent
    }
    mediaListOptions {
      scoreFormat
      rowOrder
      useLegacyLists
      animeList {
        sectionOrder
        splitCompletedSectionByFormat
        customLists
        advancedScoring
        advancedScoringEnabled
      }
      mangaList {
        sectionOrder
        splitCompletedSectionByFormat
        customLists
        advancedScoring
        advancedScoringEnabled
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (userName?: string, userId?: int)
        /// <para></para>
        /// Returns: User
        /// </summary>
        public static string GetUser => @"
query ($userId: Int, $userName: String) {
  Data: User(id: $userId, name: $userName) {
    id
    name
    about(asHtml: true)
    avatar {
      large
      medium
    }
    bannerImage
    isFollowing
    options {
      titleLanguage
      displayAdultContent
    }
    mediaListOptions {
      scoreFormat
      rowOrder
      useLegacyLists
    }
    stats {
      watchedTime
      chaptersRead
      activityHistory {
        date
        amount
        level
      }
      animeStatusDistribution {
        status
        amount
      }
      mangaStatusDistribution {
        status
        amount
      }
      animeScoreDistribution {
        score
        amount
      }
      mangaScoreDistribution {
        score
        amount
      }
    }
    donatorTier
    unreadNotificationCount
    siteUrl
    updatedAt
  }
}
";

        /// <summary>
        /// Parameters: (id: int, type: MediaType, groupCompleted: bool)
        /// <para></para>
        /// Returns: MediaListCollection with User
        /// </summary>
        public static string GetMediaListsByUserIdAndType => @"
query ($userId: Int, $type: MediaType, $groupCompleted: Boolean) {
  Data: MediaListCollection(userId: $userId, type: $type, forceSingleCompletedList: $groupCompleted) {
    user {
      id
      name
      about(asHtml: true)
      avatar {
        large
        medium
      }
      bannerImage
      isFollowing
      options {
        titleLanguage
        displayAdultContent
      }
      mediaListOptions {
        scoreFormat
        rowOrder
        useLegacyLists
        animeList {
          sectionOrder
          customLists
          advancedScoring
          splitCompletedSectionByFormat
          advancedScoringEnabled
        }
        mangaList {
          sectionOrder
          customLists
          advancedScoring
          splitCompletedSectionByFormat
          advancedScoringEnabled
        }
      }
      donatorTier
      unreadNotificationCount
      siteUrl
      updatedAt
    }
    lists {
      name
      status
      isCustomList
      isSplitCompletedList
      entries {
        id
        createdAt
        status
        score
        progress
        progressVolumes
        repeat
        priority
        notes
        hiddenFromStatusLists
        updatedAt
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
        customLists(asArray: true)
        media {
          id
          title {
            userPreferred
            english
            romaji
            native
          }
          coverImage {
            medium
            large
          }
          status
          episodes
          chapters
          volumes
          format
          type
          averageScore
          meanScore
          popularity
          genres
          nextAiringEpisode {
            id
            airingAt
            episode
            timeUntilAiring
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
        }
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (queryText: string, page: int, count: int)
        /// <para></para>
        /// Returns: PagedData of User
        /// </summary>
        public static string SearchUsers => @"
query ($queryText: String, $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: users(search: $queryText) {
      id
      name
      avatar {
        large
      }
      isFollowing
    }
  }
}
";

        /// <summary>
        /// Parameters: (animeId: int, mangaId: int, characterId: int, staffId: int, studioId: int)
        /// <para></para>
        /// Returns: UserFavourites
        /// </summary>
        public static string ToggleUserFavorite => @"
mutation ($animeId: Int, $mangaId: Int, $characterId: Int, $staffId: Int, $studioId: Int) {
  Data: ToggleFavourite(animeId: $animeId, mangaId: $mangaId, characterId: $characterId, staffId: $staffId, studioId: $studioId) {
    anime {
      nodes {
        id
      }
    }
    manga {
      nodes {
        id
      }
    }
    characters {
      nodes {
        id
      }
    }
    staff {
      nodes {
        id
      }
    }
    studios {
      nodes {
        id
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (userId: int, message: string)
        /// <para></para>
        /// Returns: MessageActivity
        /// </summary>
        public static string PostUserMessage => @"
mutation ($userId: Int, $message: String) {
  Data: SaveMessageActivity(recipientId: $userId, message: $message) {
    id
    type
    recipientId
    createdAt
    message
  }
}
";

        /// <summary>
        /// Parameters: (userId: int)
        /// <para></para>
        /// Returns: User
        /// </summary>
        public static string ToggleUserFollowing => @"
mutation ($userId: Int) {
  Data: ToggleFollow(userId: $userId) {
    id
    isFollowing
  }
}
";

        /// <summary>
        /// Paramters: (userId: int, sort: UserSort, page?: int, count?: int)
        /// <para></para>
        /// Returns: PagedData of User
        /// </summary>
        public static string GetUserFollowing => @"
query ($userId: Int!, $sort: [UserSort], $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: following(userId: $userId, sort: $sort) {
      id
      name
      avatar {
        large
      }
      isFollowing
      donatorTier
      isBlocked
    }
  }
}
";

        /// <summary>
        /// Paramters: (userId: int, sort: UserSort, page?: int, count?: int)
        /// <para></para>
        /// Returns: PagedData of User
        /// </summary>
        public static string GetUserFollowers => @"
query ($userId: Int!, $sort: [UserSort], $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: followers(userId: $userId, sort: $sort) {
      id
      name
      avatar {
        large
      }
      isFollowing
      donatorTier
      isBlocked
    }
  }
}
";
    }
}
