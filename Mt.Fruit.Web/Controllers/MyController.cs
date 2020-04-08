using Csp.Jwt;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mt.Fruit.Web.Models;
using Mt.Fruit.Web.Services;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Controllers
{
    [Authorize]
    public class MyController : Controller
    {
        private readonly IArticleService _articleService;
        private readonly IResourceService _resourceService;
        private readonly ICategoryService _categoryService;
        private readonly User _user;

        public MyController(IArticleService articleService, 
            IResourceService resourceService,
            IIdentityParser<User> parser,
            ICategoryService categoryService)
        {
            _user = parser.Parse();
            _articleService = articleService;
            _resourceService = resourceService;
            _categoryService = categoryService;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Categories(int page)
        {
            var result = await _categoryService.GetCategoryByPage("both", _user.Id, page, 20);

            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> Category(int id)
        {
            var result = await _categoryService.GetCategory(id);

            return View(result);
        }

        [HttpPost]
        public async Task<IActionResult> Category(Category category)
        {
            if (!ModelState.IsValid)
                return View(category);

            var response = await _categoryService.Create(category);
            if (response.IsSuccessStatusCode)
                return RedirectToAction(nameof(Index));
            else
                return View(category);
        }


        [HttpGet]
        public IActionResult Articles()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetArticles(int page=1)
        {
            var result = await _articleService.GetArticles(0, _user.Id, page, 20);

            return Ok(result);
        }

        /// <summary>
        /// 发布水果文章
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Create(int cid=0,int id=0)
        {
            if (cid <= 0 && id<=0)
                return NotFound();

            Article article;
            if (id > 0)
                article = await _articleService.GetArticle(id);
            else
                article = new Article() { CategoryId = cid };
            return View(article);
        }

        /// <summary>
        /// 发布趣闻杂谈
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Post(int id=0)
        {
            Article article;
            if (id > 0)
                article = await _articleService.GetArticle(id);
            else
                article = new Article() { CategoryId = 43 };

            return View(article);
        }

        [HttpGet]
        public IActionResult Pic()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Mp()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetResources(int page,string type)
        {
            var result =await _resourceService.GetResources(type, _user.Id, page, 20);

            return Ok(result);
        }

        /// <summary>
        /// 上传资源 默认上传视频
        /// </summary>
        /// <param name="cid"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Resource(int cid = 44, int id = 0)
        {
            Resource resource;
            if (id > 0)
                resource = await _resourceService.GetResource(id);
            else
                resource = new Resource { CategoryId = cid };

            return View(resource);
        }

        /// <summary>
        /// 保存趣闻轶事/水果帖子
        /// </summary>
        /// <param name="article"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Save(Article article,bool isPerview=false)
        {
            if (!ModelState.IsValid)
                if (article.CategoryId == 43)
                    return View(nameof(Post), article);
                else
                    return View(nameof(Create), article);

            article.SetId(_user.TenantId,_user.Id,_user.NickName,3,article.Id);

            //forum.Id = id;
            var response=await _articleService.Create(article);

            if (response.IsSuccessStatusCode)
            {
                var s = await response.GetResult();

                if (isPerview)
                    return Ok(s);


                if (article.Id > 0)
                {
                    return RedirectToAction(nameof(Articles));
                }
                else
                {
                    if (article.CategoryId == 43)
                        return RedirectToAction("interesting", "home");
                    else
                        return RedirectToAction("interestgroup", "home", new { id = article.CategoryId });
                }
            }

            var result = await response.GetResult();

            ModelState.AddModelError("Title", result.Msg);

            if (article.CategoryId == 43)
                return View(nameof(Post), article);

            return View(nameof(Create), article);
        }
        
        /// <summary>
        /// 保存资源 信息
        /// </summary>
        /// <param name="resource"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Commit(Resource resource)
        {
            if (!ModelState.IsValid)
                return View(nameof(Resource), resource);

            resource.SetId(_user.TenantId, _user.Id, _user.NickName, 3, resource.Id);

            //forum.Id = id;
            var response = await _resourceService.Create(resource);

            if (response.IsSuccessStatusCode)
                return RedirectToAction(resource.Id, resource.CategoryId);

            var result = await response.GetResult();

            ModelState.AddModelError("Title", result.Msg);

            return View(nameof(Resource), resource);
        }

        private IActionResult RedirectToAction(int id, int cid)
        {
            if (cid != 44)
            {
                if (id > 0)
                    return RedirectToAction(nameof(Pic));
                else
                    return RedirectToAction("interestgroup", "home", new { id = cid });
            }
            else
            {
                if (id > 0)
                    return RedirectToAction(nameof(Pic));
                else
                    return RedirectToAction("video", "home");
            }
        }
    }
}
