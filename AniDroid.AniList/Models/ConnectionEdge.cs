namespace AniDroid.AniList.Models
{
    public abstract class ConnectionEdge<TNodeType> where TNodeType : AniListObject
    {
        public int Id { get; set; }
        public TNodeType Node { get; set; }
    }
}