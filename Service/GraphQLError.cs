using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Service
{
    public class GraphQLError
    {
        public string Message { get; set; }
        public int Status { get; set; }
        public List<GraphQLErrorLocation> Locations { get; set; }

        public class GraphQLErrorLocation
        {
            public int Line { get; set; }
            public int Column { get; set; }
        }
    }
}
