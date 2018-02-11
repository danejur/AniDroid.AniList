namespace AniDroid.AniList.Interfaces
{
    public interface IAniListAuthConfig
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string RedirectUrl { get; }
        string AuthUrl { get; }
    }
}