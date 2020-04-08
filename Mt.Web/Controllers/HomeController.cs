using System.Diagnostics;
using System.Threading.Tasks;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mt.Web.Models;
using Mt.Web.Services;

namespace Mt.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IArticleService _articleService;

        public HomeController(ILogger<HomeController> logger, IArticleService articleService)
        {
            _logger = logger;
            _articleService = articleService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _articleService.GetArticles();
            return View(result);
        }

        [Route("/d/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            var articel = await _articleService.GetArticle(id,HttpContext.RemoteIp(),Request.BrowserNameByUserAgent(),Request.DeviceByUserAgent(),Request.OsByUserAgent());

            return View(articel);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
