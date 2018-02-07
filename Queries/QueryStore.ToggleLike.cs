using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal partial class QueryStore
    {
        /// <summary>
        /// Parameters: (id: int, type: LikeableType)
        /// <para></para>
        /// Returns: List of Users
        /// </summary>
        public static string ToggleLike => @"
mutation ($id: Int, $type: LikeableType) {
  Data: ToggleLike(id: $id, type: $type) {
    id
    name
    avatar {
      large
    }
  }
}
";
    }
}
