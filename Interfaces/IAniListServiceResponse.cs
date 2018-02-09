using AniDroid.AniList.GraphQL;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AniDroid.AniList.Interfaces
{
    public interface IAniListServiceResponse<T> where T : class
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
