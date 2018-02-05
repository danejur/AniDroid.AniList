using Newtonsoft.Json;
using RestSharp.Serializers;
using System;
using System.Collections.Generic;
using System.Text;

namespace AniDroid.AniList.Service
{
    public class GraphQLQuery
    {   
        public string Query { get; set; }
        public string Variables { get; set; }
    }
}
