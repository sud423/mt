using Mt.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.Web.Services
{
    public interface IArticleService
    {
        Task<Article> GetArticle(int id, string ip, string browser, string device, string os);

        Task<IEnumerable<Article>> GetArticles();

    }
}
