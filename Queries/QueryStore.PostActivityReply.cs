using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal partial class QueryStore
    {
        /// <summary>
        /// Parameters: (activityId: int, text: string)
        /// <para></para>
        /// Returns: ActivityReply
        /// </summary>
        public static string PostActivityReply => @"
mutation ($activityId: Int, $text: String) {
  Data: SaveActivityReply(activityId: $activityId, text: $text) {
    id
    userId
    activityId
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
";
    }
}
