using AniDroid.AniList.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Review : BaseAniListObject
    {
        public int UserId { get; set; }
        public int MediaId { get; set; }
        public string MediaType { get; set; }
        public string Summary { get; set; }
        public string Body { get; set; }
        public int Rating { get; set; }
        public int RatingAmount { get; set; }
        public string UserRating { get; set; }
        public int Score { get; set; }
        public bool Private { get; set; }
        public string SiteUrl { get; set; }
        public int CreatedAt { get; set; }
        public int UpdatedAt { get; set; }
        public User User { get; set; }
        public Media Media { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Review> { }

        #endregion

        #region Enum Classes

        public class ReviewRating : AniListEnum
        {
            private ReviewRating(string val, string displayVal) : base(val, displayVal) { }

            public static ReviewRating NoVote => new ReviewRating("NO_VOTE", "No Vote");
            public static ReviewRating UpVote => new ReviewRating("UP_VOTE", "Up Vote");
            public static ReviewRating DownVote => new ReviewRating("DOWN_VOTE", "Down Vote");
        }

        #endregion
    }
}
