﻿using MediatR;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Commands.Handlers
{
    public class AgreeNoticeHandler : INotificationHandler<AgreeCommand>
    {

        private readonly AppSettings _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public AgreeNoticeHandler(IOptions<AppSettings> settings, IHttpClientFactory httpClient)
        {
            _settings = settings.Value;
            _httpClient = httpClient.CreateClient("WxApiClient");
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.OcelotUrl}/api/v1/wx";
        }

        public async Task Handle(AgreeCommand notification, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(notification.ToUser))
                return;

            var notice = new
            {
                notification.ToUser,
                notification.Url,
                TemplateId = "IBe11cnTxnRvYR0dX2vYs9SC3IpqQ6bIomb68eHgNf8",
                Data = new
                {
                    first = new { value = "有人赞了您的回复！" },
                    ask = new { value = notification.Topic },
                    user = new { value = notification.NickName },
                    answer = new { value = notification.Content },
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
