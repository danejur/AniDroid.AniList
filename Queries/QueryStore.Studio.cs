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
        /// Returns: PagedData of Studio
        /// </summary>
        public static string SearchStudios => @"
query ($queryText: String, $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: studios(search: $queryText) {
      id
      name
      isFavourite
    }
  }
}
";
    }
}
