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

        /// <summary>
        /// Parameters: (id: int)
        /// <para></para>
        /// Returns: Staff
        /// </summary>
        public static string GetStaffById => @"
query ($id: Int) {
  Data: Staff(id: $id) {
    id
    name {
      first
      last
      native
    }
    image {
      large
    }
    description(asHtml: true)
    isFavourite
    siteUrl
    language
    staffMedia {
      edges {
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
        staffRole
      }
    }
    characters {
      edges {
        node {
          id
          name {
            first
            last
            alternative
            native
          }
          image {
            large
          }
          isFavourite
        }
        role
        media {
          id
          title {
            userPreferred
          }
          format
          coverImage {
            large
          }
        }
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (staffId: int, page: int, perPage: int)
        /// <para></para>
        /// Returns: Staff with PagedData of Media
        /// </summary>
        public static string GetStaffMedia => @"
query ($staffId: Int, $page: Int, $perPage: Int) {
  Data: Staff(id: $staffId) {
    staffMedia(page: $page, perPage: $perPage) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
        hasNextPage
      }
      edges {
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
        staffRole
      }
    }
  }
}
";

        /// <summary>
        /// Parameters: (staffId: int, page: int, perPage: int)
        /// <para></para>
        /// Returns: Staff with PagedData of Characters
        /// </summary>
        public static string GetStaffCharacters => @"
query ($staffId: Int, $page: Int, $perPage: Int) {
  Data: Staff(id: $staffId) {
    characters(page: $page, perPage: $perPage) {
      pageInfo {
        total
        perPage
        currentPage
        lastPage
        hasNextPage
      }
      edges {
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
    }
}
