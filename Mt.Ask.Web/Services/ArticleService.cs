using Csp.Web;
using Csp.Web.Extensions;
using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Ask.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public class ArticleService : IArticleService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public ArticleService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/blog/api/v1";
        }


        public async Task<OptResult> Create(Article article)
        {
            string uri = API.Article.Create(_remoteServiceBaseUrl);

            var forumContent = new StringContent(article.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response= await _httpClient.PostAsync(uri, forumContent);

            return await response.GetResult();

        }

        public async Task<OptResult> Delete(int id)
        {
            string uri = API.Article.Delete(_remoteServiceBaseUrl, id);

            var response= await _httpClient.DeleteAsync(uri);

            return await response.GetResult();
        }

        public async Task<OptResult> DeleteReply(int replyId)
        {
            string uri = API.Article.DeleteReply(_remoteServiceBaseUrl, replyId);

            var response = await _httpClient.DeleteAsync(uri);

            return await response.GetResult();
        }

        public async Task<Article> GetArticle(int id, string ip, string browser, string device, string os, int userId = 0)
        {
            var browse = new BrowseHistory(id, ip, browser, os, device,userId);

            string uri = API.Article.GetArticle(_remoteServiceBaseUrl);

            var forumContent = new StringContent(browse.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);
            
            var result = await response.GetResult<Article>();

            return result;
        }

        public async Task<Article> GetArticle(int id)
        {
            string uri = API.Article.GetArticle(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<Article>();

            return result;
        }

        public async Task<PagedResult<Article>> GetArticleByPage(int categoryId, int userId, int page, int size)
        {
            string uri = API.Article.GetArticleByPage(_remoteServiceBaseUrl, categoryId, userId, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Article>>();

            return result;
        }

        public async Task<IEnumerable<Article>> GetArticles(int categoryId)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl, categoryId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Article>>();

            return result;
        }

        public async Task<IEnumerable<Article>> GetArticles(int categoryId,int size)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl,categoryId,size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Article>>();

            return result;
        }

        public async Task<PagedResult<Reply>> GetReplies(int id, int page, int size)
        {
            string uri = API.Article.GetReplyByPage(_remoteServiceBaseUrl, id, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Reply>>();

            return result;
        }

        public async Task<WxConfig> GetWxConfig(string url)
        {
            string uri = $"{_settings.Value.OcelotUrl}/api/v1/wx/getconfig?url={url}";

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<WxConfig>();

            return result;
        }
    }
}
