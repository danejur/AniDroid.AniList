namespace AniDroid.AniList.Models
{
    public class FavoriteDto
    {
        public int? AnimeId { get; set; }
        public int? MangaId { get; set; }
        public int? CharacterId { get; set; }
        public int? StaffId { get; set; }

        [Newtonsoft.Json.JsonIgnore]
        public bool HasValue
            => AnimeId.HasValue && MangaId.HasValue && CharacterId.HasValue && StaffId.HasValue;
    } // TODO: Add Missing Id
}
