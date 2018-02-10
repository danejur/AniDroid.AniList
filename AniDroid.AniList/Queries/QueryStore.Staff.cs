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
        /// Returns: PagedData of Staff
        /// </summary>
        public static string SearchStaff => @"
query ($queryText: String, $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: staff(search: $queryText) {
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
";
    }
}
