using Csp.Jwt;
using Csp.Web;
using Csp.Web.Extensions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mt.Ask.Web.Commands;
using Mt.Ask.Web.Models;
using Mt.Ask.Web.Services;
using System.Threading.Tasks;

namespace Mt.Ask.Web.Controllers
{
    [Authorize]
    public class ForumController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IArticleService _articleService;
        private readonly User _user;


        public ForumController(IMediator mediator, IArticleService articleService,IIdentityParser<User> parser)
        {
            _mediator = mediator;
            _articleService = articleService;
            _user = parser.Parse();
        }

        public async Task<IActionResult> Index(int page = 1)
        {
            var result =await _articleService.GetArticleByPage(1, 0, page, 20);

            return View(result);
        }
        
        public async Task<IActionResult> List(int page = 1)
        {
            var result =await _articleService.GetArticleByPage(1, _user.Id, page, 20);

            return View(result);
        }

        public async Task<IActionResult> Post(int? id)
        {
            ViewBag.Id = id.GetValueOrDefault();
            if (id.HasValue && id > 0)
            {
                var vm = await _articleService.GetArticle(id.Value);
                return View(vm);
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Save(int id, Article article)
        {
            if (!ModelState.IsValid)
                return View(nameof(Post), article);

            article.SetId(_user.TenantId, _user.Id,_user.NickName, 1, id);//不能当前登录的用户的注册站点编号

            var result=await _articleService.Create(article);

            if (result.Succeed)
                return RedirectToAction(nameof(List));

            ModelState.AddModelError("Title", result.Msg);

            return View(nameof(Post), article);

        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var article = await _articleService.GetArticle(id, HttpContext.RemoteIp(),
                Request.BrowserNameByUserAgent(), Request.DeviceByUserAgent(), 
                Request.OsByUserAgent(),_user.Id); 

#if !DEBUG
            var config = await _articleService.GetWxConfig(Request.GetCurrentUrl());
            
            ViewBag.WxConfig = config;
#endif
            return View(article);
        }


        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _articleService.Delete(id);
            
            return Ok(result);
        }

        public async Task<IActionResult> GetReplies(int id, int page=1)
        {
            return Ok(await _articleService.GetReplies(id, page, 20));
        }

        [HttpPost]
        public async Task<IActionResult> Reply(ReplyCommand request)
        {
            request.UserId = _user.Id;

            await _mediator.Publish(request);
            return Ok(OptResult.Success());
        }

        [HttpPost]
        public async Task<ActionResult> Agree(AgreeCommand request)
        {
            request.UserId = _user.Id;

            await _mediator.Publish(request);
            return Ok(OptResult.Success());
        }

        [HttpPost]
        public async Task<ActionResult> DelReply(int id)
        {
            var result = await _articleService.DeleteReply(id);
            return Ok(result);
        }
    }
}
