using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Commands.Handlers
{
    public class ReplyCommandHandler : INotificationHandler<ReplyCommand>
    {
        private readonly AppSettings _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public ReplyCommandHandler(IOptions<AppSettings> settings, IHttpClientFactory httpClient)
        {
            _settings = settings.Value;
            _httpClient = httpClient.CreateClient("ArticleClient");
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.OcelotUrl}/blog/api/v1";
        }

        public async Task Handle(ReplyCommand notification, CancellationToken cancellationToken)
        {
            var reply = new
            {
                Id = notification.ReplyId,
                SourceId = notification.ArticleId,
                Source = "article",
                notification.Content,
                notification.UserId
            };

            string uri = $"{_remoteServiceBaseUrl}/article/reply";

            var forumContent = new StringContent(JsonConvert.SerializeObject(reply), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            response.EnsureSuccessStatusCode();
        }
    }
}
