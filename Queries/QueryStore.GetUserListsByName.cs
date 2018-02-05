using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        public const string GetMediaListsByUserNameAndType = @"
query ($name: String, $type: MediaType) {
  Data: MediaListCollection(userName: $name, type: $type) {
    user {
      mediaListOptions {
        scoreFormat
        animeList {
          sectionOrder
          customLists
        }
        mangaList {
          sectionOrder
          customLists
        }
        rowOrder
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
        }
        coverImage {
          medium
          large
        }
        episodes
        chapters
        volumes
        format
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
