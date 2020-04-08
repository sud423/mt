using Csp.Web.Extensions;
using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public CategoryService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/blog/api/v1";
        }

        public async Task<HttpResponseMessage> Create(Category category)
        {
            string uri = API.Category.Create(_remoteServiceBaseUrl);
            StringContent content = new StringContent(category.ToJson(), Encoding.UTF8, "application/json");
            return await _httpClient.PostAsync(uri, content);
        }

        public async Task<IEnumerable<Category>> GetCategories(string type)
        {
            string uri = API.Category.GetCategories(_remoteServiceBaseUrl, type);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Category>>();

            return result;
        }

        public async Task<Category> GetCategory(int id)
        {
            string uri = API.Category.GetCategory(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<Category>();

            return result;
        }

        public async Task<PagedResult<Category>> GetCategoryByPage(string type, int userId, int page, int size)
        {
            string uri = API.Category.GetCategoryByPage(_remoteServiceBaseUrl, userId, type, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Category>>();

            return result;
        }

        public async Task<IEnumerable<Category>> GetHotCategories(string type)
        {
            string uri = API.Category.GetHotCategories(_remoteServiceBaseUrl, type);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Category>>();

            return result;
        }
    }
}
