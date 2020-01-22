using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Models
{
    public class Recommendation : AniListObject
    {
        public int Rating { get; set; }
        public Media Media { get; set; }
        public Media MediaRecommendation { get; set; }
        public User User { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<Recommendation> { }

        #endregion

        #region Enum Classes

        public sealed class RecommendationRating : AniListEnum
        {
            private RecommendationRating(string val, string displayVal, int index) : base(val, displayVal, index) { }

            public static RecommendationRating NoRating { get; } = new RecommendationRating("NO_RATING", "No Rating", 0);
            public static RecommendationRating RateUp { get; } = new RecommendationRating("RATE_UP", "Rate Up", 1);
            public static RecommendationRating RateDown { get; } = new RecommendationRating("RATE_DOWN", "Rate Down", 2);
        }

        #endregion
    }
}
