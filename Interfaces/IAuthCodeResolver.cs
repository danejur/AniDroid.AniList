using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Interfaces
{
    public interface IAuthCodeResolver
    {
        string AuthCode { get; }
        bool IsAuthorized { get; }
    }
}
