using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Service
{
    public class GraphQLResponse<T>
    {
        public Dictionary<string, T> Data { get; set; }
        public List<GraphQLError> Errors { get; set; }
        public T Value => Data["Data"];
    }
}
