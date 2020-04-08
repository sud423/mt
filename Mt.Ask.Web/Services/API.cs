namespace Mt.Ask.Web.Services
{
    public static partial class API
    {
        public static class Article
        {
            public static string Create(string baseUrl) => $"{baseUrl}/article/create";

            public static string Delete(string baseUrl,int id) => $"{baseUrl}/article/delete/{id}";

            public static string DeleteReply(string baseUrl, int replyId) => $"{baseUrl}/article/delreply/{replyId}";

            public static string GetArticle(string baseUrl) => $"{baseUrl}/articles/browse";

            public static string GetArticle(string baseUrl,int id) => $"{baseUrl}/article/find/{id}";

            public static string GetArticles(string baseUrl,int categoryId,int size) => $"{baseUrl}/articles/2/{categoryId}/1/{size}";

            public static string GetArticles(string baseUrl, int categoryId) => $"{baseUrl}/articles/2/{categoryId}/1";

            public static string GetArticleByPage(string baseUrl,int categoryId, int userId, int page, int size)
                => $"{baseUrl}/article/2/1/{categoryId}?userId={userId}&page={page}&size={size}";

            public static string GetReplyByPage(string baseUrl, int id, int page, int size)
                => $"{baseUrl}/article/getreplies/{id}?page={page}&size={size}";


        }

        public static class Announce
        {
            public static string GetAnnounce(string baseUrl,int id) => $"{baseUrl}/announces/find/{id}";

            public static string GetAnnounces(string baseUrl, int size) => $"{baseUrl}/announces/2?size={size}";
        }

        public static class Course
        {
            public static string GetCourse(string baseUrl, int id) => $"{baseUrl}/courses/find/{id}";

            public static string GetCourses(string baseUrl, int size) => $"{baseUrl}/courses/2/{size}";

            public static string GetCoursesByCondtion(string baseUrl, string academy, string classify) 
                => $"{baseUrl}/courses?tenantId=2&academy={academy}&classify={classify}";

            public static string GetHotCourses(string baseUrl) => $"{baseUrl}/courses/2";

        }

        public static class Auth
        {
            public static string GetAuthUrl(string baseUrl, string redirectUrl) => $"{baseUrl}/getauth?url={redirectUrl}";

            public static string GetUser(string baseUrl, string code) =>$"{baseUrl }/wxlogin/2/1/{code}";

            public static string BindCell(string baseUrl, string cell,int userId) => $"{baseUrl }/bind/{userId}/{cell}";

            public static string UserLogin(string baseUrl) => $"{baseUrl}/signin";
        }
    }
}
