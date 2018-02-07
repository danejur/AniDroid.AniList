using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        /// <summary>
        /// Parameters: (name: string, type: MediaType)
        /// <para></para>
        /// Returns: MediaListCollection
        /// </summary>
        public static string GetMediaListsByUserNameAndType => @"
query ($name: String, $type: MediaType) {
  Data: MediaListCollection(userName: $name, type: $type) {
    user {
      name
      mediaListOptions {
        scoreFormat
        animeList {
          customLists
          sectionOrder
        }
        mangaList {
          customLists
          sectionOrder
        }
      }
    }
    statusLists(asArray: true) {
      status
      score
      progress
      progressVolumes
      repeat
      priority
      notes
      hiddenFromStatusLists
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
      customLists
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
        averageScore
        meanScore
        popularity
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
";
    }
}
