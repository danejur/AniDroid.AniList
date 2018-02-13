using System;
using System.Collections.Generic;
using System.Text;
using AniDroid.AniList.GraphQL;
using AniDroid.AniList.Interfaces;

namespace AniDroid.AniList.Service
{
    public class AniListError : IAniListError
    {
        public AniListError(string errorMessage, Exception errorException, List<GraphQLError> graphQLErrors)
        {
            ErrorMessage = errorMessage;
            ErrorException = errorException;
            GraphQLErrors = graphQLErrors;
        }

        public string ErrorMessage { get; }
        public Exception ErrorException { get; }
        public List<GraphQLError> GraphQLErrors { get; }
    }
}
