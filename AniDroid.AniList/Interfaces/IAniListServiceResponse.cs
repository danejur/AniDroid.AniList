using AniDroid.AniList.GraphQL;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListServiceResponse<out T> where T : class
    {
        T Data { get; }
        bool IsSuccessful { get; }
        ResponseStatus ResponseStatus { get; }
        HttpStatusCode StatusCode { get; }
        string ErrorMessage { get; }
        Exception ErrorException { get; }
        List<GraphQLError> GraphQLErrors { get; }
    }
}
