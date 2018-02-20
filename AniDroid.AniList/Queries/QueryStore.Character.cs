using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal partial class QueryStore
    {
        /// <summary>
        /// Parameters: (queryText: string, page: int, count: int)
        /// <para></para>
        /// Returns: PagedData of Character
        /// </summary>
        public static string SearchCharacters => @"
query ($queryText: String, $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: characters(search: $queryText) {
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
      isFavourite
    }
  }
}
";

        /// <summary>
        /// Parameters: (id: int)
        /// <para></para>
        /// Returns: Character with PageInfo for Media
        /// </summary>
        public static string GetCharacterById => @"
query ($id: Int) {
  Data: Character(id: $id) {
    id
    name {
      first
      last
      native
      alternative
    }
    image {
      large
      medium
    }
    description(asHtml: true)
    isFavourite
    siteUrl
    media(perPage: 25) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
      }
      edges {
        id
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (characterId: int, page: int, perPage: int)
        /// <para></para>
        /// Returns: Character with PagedData of Media with Staff
        /// </summary>
        public static string GetCharacterMedia => @"
query ($characterId: Int, $page: Int, $perPage: Int) {
  Data: Character(id: $characterId) {
    media(page: $page, perPage: $perPage) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
        hasNextPage
      }
      edges {
        characterRole
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
          isFavourite
        }
        node {
          id
          title {
            userPreferred
          }
          coverImage {
            large
          }
          format
          type
        }
      }
    }
  }
}
";
    }
}
