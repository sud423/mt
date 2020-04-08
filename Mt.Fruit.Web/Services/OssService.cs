using Csp.Web;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Services
{
    public class OssService : IOssService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public OssService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/oss/upload";
        }

        public async Task<OptResult> Upload(IFormFile file,string dir)
        {

            var multipartContent = new MultipartFormDataContent
            {
                {
                    new StreamContent(file.OpenReadStream())
                    {
                        Headers =
                        {
                            ContentLength = file.Length,
                            ContentType = new MediaTypeHeaderValue(file.ContentType),
                            ContentDisposition=new ContentDispositionHeaderValue("form-data"){ Name = "file", FileName = file.FileName }
                        }
                    }
                },
                { new StringContent(dir, Encoding.UTF8), "key" }
            };


            var response = await _httpClient.PostAsync(_remoteServiceBaseUrl, multipartContent).ConfigureAwait(false);

            return await response.GetResult();
        }
    }
}
