using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Commands.Handlers
{
    public class ReplyNoticeHandler : INotificationHandler<ReplyCommand>
    {
        private readonly AppSettings _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public ReplyNoticeHandler(IOptions<AppSettings> settings, IHttpClientFactory httpClient)
        {
            _settings = settings.Value;
            _httpClient = httpClient.CreateClient("WxApiClient");
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.OcelotUrl}/api/v1/wx";
        }

        public async Task Handle(ReplyCommand notification, CancellationToken cancellationToken)
        {
            if (notification.ReplyId > 0)
                return;

            var notice = new
            {
                notification.ToUser,
                notification.Url,
                TemplateId = "KFtsctyGuG-QXtmdnlQrlR1GlkPfekdLhPoo6b36etc",
                Data = new
                {
                    first = new { value = "您在我爱8点发的主题有新的回复！" },
                    keyword1 = new { value = notification.Topic },
                    keyword2 = new { value = notification.Content },
                    remark = new { value = "欢迎您及时关注最新回复，点击查看详情。" }
                }
            };

            string uri = $"{_remoteServiceBaseUrl}/send";

            var forumContent = new StringContent(JsonConvert.SerializeObject(notice), System.Text.Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(uri, forumContent);

            response.EnsureSuccessStatusCode();

        }
    }
}
