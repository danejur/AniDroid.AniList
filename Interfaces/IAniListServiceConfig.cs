using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListServiceConfig
    {
        string ClientId { get; }
        string ClientSecret { get; }
        string BaseUrl { get; }
        string RedirectUrl { get; }
        string AuthUrl { get; }
    }
}
