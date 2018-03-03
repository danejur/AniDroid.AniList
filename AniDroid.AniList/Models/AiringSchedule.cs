namespace AniDroid.AniList.Models
{
    public class AiringSchedule : AniListObject
    {
        public int AiringAt { get; set; }
        public int TimeUntilAiring { get; set; }
        public int Episode { get; set; }
        public int MediaId { get; set; }
        public Media Media { get; set; }

        #region Internal Classes

        public class Edge : ConnectionEdge<AiringSchedule> { }

        #endregion
    }
}
