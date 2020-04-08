using Csp.Web;
using Csp.Web.Mvc.Paging;
using Mt.Ask.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public interface IArticleService
    {

        Task<OptResult> Create(Article article);

        Task<OptResult> Delete(int id);

        Task<OptResult> DeleteReply(int replyId);

        Task<Article> GetArticle(int id, string ip, string browser, string device, string os, int userId = 0);

        Task<Article> GetArticle(int id);

        Task<PagedResult<Article>> GetArticleByPage(int categoryId, int userId, int page, int size);

        Task<IEnumerable<Article>> GetArticles(int categoryId);

        Task<IEnumerable<Article>> GetArticles(int categoryId, int size);

        Task<PagedResult<Reply>> GetReplies(int id, int page, int size);

        Task<WxConfig> GetWxConfig(string url);

    }
}
