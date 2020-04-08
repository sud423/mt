namespace Mt.Fruit.Web.Services
{
    public static partial class API
    {
        public static class Category
        {
            public static string Create(string baseUrl) => $"{baseUrl}/category/create";

            public static string GetCategories(string baseUrl, string type) => $"{baseUrl}/categories/2/3/{type}";
            
            public static string GetCategory(string baseUrl, int id) => $"{baseUrl}/categories/find/{id}";
            
            public static string GetCategoryByPage(string baseUrl, int userId,string type,int page,int size) 
                => $"{baseUrl}/category/2/{type}?userId={userId}&page={page}&size={size}";

            public static string GetHotCategories(string baseUrl, string type) => $"{baseUrl}/categories/hot/2/3/{type}";

        }
        public static class Article
        {
            public static string Create(string baseUrl) => $"{baseUrl}/article/create";

            public static string Delete(string baseUrl, int id) => $"{baseUrl}/article/delete/{id}";

            public static string GetArticle(string baseUrl) => $"{baseUrl}/articles/browse";

            public static string GetArticle(string baseUrl,int id) => $"{baseUrl}/article/find/{id}";

            public static string GetArticles(string baseUrl, int categoryId,int page, int size) 
                => $"{baseUrl}/articles/2/{categoryId}/3/{page}/{size}";

            public static string GetArticles(string baseUrl, int categoryId, int page, int size, int userId)
                => $"{baseUrl}/article/2/3/{categoryId}?userId={userId}&page={page}&size={size}";
        }

        public static class Resource
        {
            public static string Create(string baseUrl) => $"{baseUrl}/resource/create";

            public static string Delete(string baseUrl, int id) => $"{baseUrl}/resource/delete/{id}";

            public static string GetResource(string baseUrl) => $"{baseUrl}/resources/browse";

            public static string GetResource(string baseUrl,int id) => $"{baseUrl}/resource/find/{id}";

            public static string GetResources(string baseUrl, int categoryId, int page, int size) => $"{baseUrl}/resources/2/{categoryId}/3/{page}/{size}";

            public static string GetResources(string baseUrl, string type,int userId, int page, int size) => $"{baseUrl}/resource/{type}/2/{userId}?page={page}&size={size}";

        }

        public static class Auth
        {
            public static string Create(string baseUrl) => $"{baseUrl}/create";

            public static string UserLogin(string baseUrl) => $"{baseUrl}/signin";
        }

    }
}
