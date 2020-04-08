using Csp.Web.Extensions;
using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
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


        public async Task<HttpResponseMessage> Create(Article article)
        {
            string uri = API.Article.Create(_remoteServiceBaseUrl);

            var forumContent = new StringContent(article.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            string uri = API.Article.Delete(_remoteServiceBaseUrl, id);

            var response = await _httpClient.DeleteAsync(uri);

            return response;
        }

        public async Task<Article> GetArticle(int id, string ip, string browser, string device, string os, int userId = 0)
        {
            var browse = new BrowseHistory(id, ip, browser, os, device,"article", userId);

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

        public async Task<PagedResult<Article>> GetArticles(int categoryId, int page, int size)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl, categoryId,page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Article>>();

            return result;
        }

        public async Task<PagedResult<Article>> GetArticles(int categoryId, int userId, int page, int size)
        {
            string uri = API.Article.GetArticles(_remoteServiceBaseUrl, categoryId, page, size,userId);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Article>>();

            return result;
        }
    }
}
