using Csp.Web.Extensions;
using Microsoft.Extensions.Options;
using Mt.Ask.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public class AnnounceService : IAnnounceService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public AnnounceService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/ask/api/v1";
        }

        public async Task<Article> GetAnnounce(int id)
        {
            string uri = API.Announce.GetAnnounce(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var response = responseString.FromJson<Article>();

            return response;
        }

        public async Task<IEnumerable<Article>> GetAnnounces(int size)
        {
            string uri = API.Announce.GetAnnounces(_remoteServiceBaseUrl, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var response = responseString.FromJson<IEnumerable<Article>>();

            return response;
        }
    }
}
