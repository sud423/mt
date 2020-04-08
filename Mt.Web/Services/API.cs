namespace Mt.Web.Services
{
    public static partial class API
    {
        public static class Article
        {
            public static string GetArticles(string baseUrl) => $"{baseUrl}/articles/2/42/2/10";

            public static string GetArticle(string baseUrl) => $"{baseUrl}/articles/browse";
        }
    }
}
