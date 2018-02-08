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
        /// Returns: PagedData of ForumThread
        /// </summary>
        public static string SearchForumThreads => @"
query ($queryText: String, $page: Int, $count: Int) {
  Data: Page(page: $page, perPage: $count) {
    pageInfo {
      total
      perPage
      currentPage
      lastPage
      hasNextPage
    }
    Data: threads(search: $queryText) {
      id
      title
      replyCount
      siteUrl
      updatedAt
      user {
        id
        avatar{
          large
        }
        name
      }
    }
  }
}
";
    }
}
