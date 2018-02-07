using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Queries
{
    internal partial class QueryStore
    {
        public static string GetAniListActivityById => @"
query ($id: Int) {
  Data: Activity(id: $id) {
    ... on TextActivity {
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
    ... on ListActivity {
      id
      userId
      type
      status
      progress
      replyCount
      createdAt
      user {
        id
        name
        avatar {
          large
        }
      }
      media {
        id
        title {
          userPreferred
        }
        coverImage {
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
    ... on MessageActivity {
      id
      type
      replyCount
      createdAt
      message
      messenger {
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
}
";
    }
}
