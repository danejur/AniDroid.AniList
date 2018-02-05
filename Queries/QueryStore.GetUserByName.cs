using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal static partial class QueryStore
    {
        public const string GetUserByName = @"query ($name: String) {
  Data: User(name: $name) {
    id
    name
    about(asHtml: true)
    avatar {
      large
      medium
    }
    bannerImage
    isFollowing
    options {
      titleLanguage
      displayAdultContent
    }
    mediaListOptions {
      scoreFormat
      rowOrder
      useLegacyLists
    }
    donatorTier
    unreadNotificationCount
    siteUrl
    updatedAt
  }
}
";
    }
}
