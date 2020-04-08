using MediatR;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Commands.Handlers
{
    public class AgreeHandler : INotificationHandler<AgreeCommand>
    {
        private readonly AppSettings _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public AgreeHandler(IOptions<AppSettings> settings, IHttpClientFactory httpClient)
        {
            _settings = settings.Value;
            _httpClient = httpClient.CreateClient("ArticleClient");
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.OcelotUrl}/blog/api/v1";
        }
        public async Task Handle(AgreeCommand notification, CancellationToken cancellationToken)
        {
            string uri = $"{_remoteServiceBaseUrl}/article/agree/{notification.ReplyId}/{notification.UserId}";

            var response = await _httpClient.PutAsync(uri, null);

            response.EnsureSuccessStatusCode();
        }
    }
}
