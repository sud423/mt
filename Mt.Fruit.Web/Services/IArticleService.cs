using Csp.Web.Mvc.Paging;
using Mt.Fruit.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public interface IArticleService
    {

        Task<HttpResponseMessage> Create(Article article);

        Task<HttpResponseMessage> Delete(int id);

        Task<Article> GetArticle(int id, string ip, string browser, string device, string os, int userId = 0);

        Task<Article> GetArticle(int id);

        Task<PagedResult<Article>> GetArticles(int categoryId,int page, int size);

        Task<PagedResult<Article>> GetArticles(int categoryId,int userId, int page, int size);

    }
}
