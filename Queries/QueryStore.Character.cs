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
        /// Returns: Character
        /// </summary>
        public static string GetCharacterById => @"
query ($id:Int) {
  Data: Character(id:$id) {
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
      }
      edges {
        node {
          id
          title {
            userPreferred
          }
          format
          type
        }
        relationType
        isMainStudio
        characterRole
        voiceActors {
          id
          name {
            first
            last
            native
          }
          language
          image {
            large
          }
          isFavourite
        }
      }
    }
  }
}
";
    }
}
