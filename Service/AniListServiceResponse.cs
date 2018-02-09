using AniDroid.AniList.GraphQL;
using AniDroid.AniList.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace AniDroid.AniList.Service
{
    public class AniListServiceResponse<T> : IAniListServiceResponse<T> where T : class
    {
        private AniListServiceResponse() { }

        internal static AniListServiceResponse<T> CreateResponse(IRestResponse<GraphQLResponse<T>> restResp)
        {
            return new AniListServiceResponse<T>
            {
                IsSuccessful = restResp.IsSuccessful,
                ErrorException = restResp.ErrorException,
                ErrorMessage = restResp.ErrorMessage,
                ResponseStatus = restResp.ResponseStatus,
                StatusCode = restResp.StatusCode,
                GraphQLErrors = restResp.Data?.Errors,
                Data = restResp.Data?.Value
            };
        }

        public T Data { get; private set; }
        public bool IsSuccessful { get; private set; }
        public ResponseStatus ResponseStatus { get; private set; }
        public HttpStatusCode StatusCode { get; private set; }
        public string ErrorMessage { get; private set; }
        public Exception ErrorException { get; private set; }
        public List<GraphQLError> GraphQLErrors { get; private set; }
    }
}
