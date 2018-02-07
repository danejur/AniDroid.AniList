using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal partial class QueryStore
    {
        /// <summary>
        /// Parameters: (text: string)
        /// <para></para>
        /// Returns: AniListActivity
        /// </summary>
        public static string PostTextActivity => @"
mutation ($text: String) {
  Data: SaveTextActivity(text: $text) {
    id
    userId
    type
    replyCount
    text
    createdAt
    user {
      id
      name
      avatar {
        large
      }
    }
    likes {
      id
      name
      avatar {
        large
      }
    }
    replies {
      id
      text
      createdAt
      user {
        id
        name
        avatar {
          large
        }
      }
      likes {
        id
        name
        avatar {
          large
        }
      }
    }
  }
}
";
    }
}
