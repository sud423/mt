using Csp.Web.Extensions;
using Microsoft.Extensions.Options;
using Mt.Ask.Web.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Services
{
    public class CourseService : ICourseService
    {
        private readonly IOptions<AppSettings> _settings;

        private readonly HttpClient _httpClient;

        private readonly string _remoteServiceBaseUrl;

        public CourseService(IOptions<AppSettings> settings, HttpClient httpClient)
        {
            _settings = settings;
            _httpClient = httpClient;
            //_logger = logger;

            _remoteServiceBaseUrl = $"{_settings.Value.OcelotUrl}/ask/api/v1";
        }

        public async Task<Course> GetCourse(int id)
        {
            string uri = API.Course.GetCourse(_remoteServiceBaseUrl, id);

            var responseString = await _httpClient.GetStringAsync(uri);

            var response = responseString.FromJson<Course>();

            return response;
        }

        public async Task<IEnumerable<Course>> GetCourses(int size)
        {
            string uri = API.Course.GetCourses(_remoteServiceBaseUrl, size);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Course>>();

            return result;
        }

        public async Task<IEnumerable<Course>> GetCoursesByCondtion(string academy, string classify)
        {
            string uri = API.Course.GetCoursesByCondtion(_remoteServiceBaseUrl, academy,classify);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Course>>();

            return result;
        }

        public async Task<IEnumerable<Course>> GetHotCourses()
        {
            string uri = API.Course.GetHotCourses(_remoteServiceBaseUrl);

            var responseString = await _httpClient.GetStringAsync(uri);

            var result = responseString.FromJson<IEnumerable<Course>>();

            return result;
        }
    }
}
