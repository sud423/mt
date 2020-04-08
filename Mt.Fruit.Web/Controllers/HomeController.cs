using System.Diagnostics;
using System.Threading.Tasks;
using Csp.Jwt;
using Csp.Web;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;

namespace Mt.Fruit.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        private readonly IResourceService _resourceService;
        private readonly User _user;

        public HomeController(ILogger<HomeController> logger, 
            ICategoryService categoryService,
            IArticleService articleService,
            IResourceService resourceService,
            IIdentityParser<User> parser)
        {
            _logger = logger;

            _user = parser.Parse();

            _articleService = articleService;
            _categoryService = categoryService;
            _resourceService = resourceService;
        }

        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetHotCategories("both");

            return View(result);
        }

        [Route("/group")]
        public async Task<IActionResult> Group()
        {
            var result = await _categoryService.GetCategories("both");

            return View(result);
        }

        [Route("/interestgroup/{id:int}")]
        async public Task<IActionResult> InterestGroup(int id)
        {
            ViewBag.CategoryId = id;
            var result = await _categoryService.GetCategory(id);

            return View(result);
        }

        [Route("/interesting")]
        public IActionResult Interesting()
        {
            return View();
        }

        [Route("/detail/{id:int}")]
        public async Task<IActionResult> Detail(int id)
        {
            if (id <= 0)
                return NotFound();

            var article = await _articleService.GetArticle(id, HttpContext.RemoteIp(),
                Request.BrowserNameByUserAgent(), Request.DeviceByUserAgent(),
                Request.OsByUserAgent(), _user.Id);

            return View(article);
        }

        [Route("/video")]
        public IActionResult Video()
        {
            return View();
        }

        [Route("/list/{categoryId:int}")]
        public async Task<IActionResult> List(int categoryId,int page = 1)
        {
            var result = await _articleService.GetArticles(categoryId, page, 30);

            return Ok(result);
        }

        [Route("/list/{categoryId:int}/{page:int}")]
        public async Task<IActionResult> Resource(int categoryId,int page = 1,int size=36)
        {
            var result = await _resourceService.GetResources(categoryId, page, 36);

            return Ok(result);
        }

        [HttpGet,Route("/read/{id:int}")]
        public async Task<IActionResult> Look(int id)
        {
            if (id <= 0)
                return Ok(OptResult.Failed("资源不存在"));

            await _resourceService.GetResource(id, HttpContext.RemoteIp(),
                Request.BrowserNameByUserAgent(), Request.DeviceByUserAgent(),
                Request.OsByUserAgent(), _user.Id);

            return Ok(OptResult.Success());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
