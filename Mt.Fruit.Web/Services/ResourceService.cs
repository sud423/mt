using Csp.Web.Extensions;
using Csp.Web.Mvc.Paging;
using Microsoft.Extensions.Options;
using Mt.Fruit.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public class ResourceService : IResourceService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public ResourceService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/blog/api/v1";
        }

        public async Task<HttpResponseMessage> Create(Resource resource)
        {
            string uri = API.Resource.Create(_remoteServiceBaseUrl);

            var forumContent = new StringContent(resource.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            return response;
        }

        public async Task<HttpResponseMessage> Delete(int id)
        {
            string uri = API.Resource.Delete(_remoteServiceBaseUrl, id);

            var response = await _httpClient.DeleteAsync(uri);

            return response;
        }

        public async Task<Resource> GetResource(int id, string ip, string browser, string device, string os, int userId = 0)
        {
            var browse = new BrowseHistory(id, ip, browser, os, device,"resource", userId);

            string uri = API.Resource.GetResource(_remoteServiceBaseUrl);

            var forumContent = new StringContent(browse.ToJson(), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            var result = await response.GetResult<Resource>();

            return result;
        }

        public async Task<Resource> GetResource(int id)
        {
            string uri = API.Resource.GetResource(_remoteServiceBaseUrl, id);
            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<Resource>();

            return result;
        }

        public async Task<PagedResult<Resource>> GetResources(int categoryId, int page, int size)
        {
            string uri = API.Resource.GetResources(_remoteServiceBaseUrl, categoryId, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Resource>>();

            return result;
        }

        public async Task<PagedResult<Resource>> GetResources(string type, int userId, int page, int size)
        {
            string uri = API.Resource.GetResources(_remoteServiceBaseUrl, type,userId, page, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<PagedResult<Resource>>();

            return result;
        }
    }
}
