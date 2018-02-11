namespace AniDroid.AniList.GraphQL
{
    public class GraphQLQuery
    {
        public string Query { get; set; }
        public object Variables { get; set; }

        public GraphQLQuery()
        { }

        public GraphQLQuery(string query, object variables)
        {
            Query = query;
            Variables = variables;
        }
    }
}
