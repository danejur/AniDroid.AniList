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
        public static string SearchCharacter => @"
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
    }
}
