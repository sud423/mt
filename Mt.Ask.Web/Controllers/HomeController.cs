using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Csp.Web.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mt.Ask.Web.Models;
using Mt.Ask.Web.Services;

namespace Mt.Ask.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly IAnnounceService _announceService;
        private readonly IArticleService _articleService;
        private readonly ICourseService _courseService;
        
        private readonly IHostEnvironment _hostEnvironment;

        private Dictionary<string, string> yx = new Dictionary<string, string>();
        private Dictionary<string, string> kc = new Dictionary<string, string>();
        private Dictionary<string, string> qt = new Dictionary<string, string>();

        public HomeController(ILogger<HomeController> logger, IAnnounceService announceService, 
            IArticleService articleService, ICourseService courseService, IHostEnvironment hostEnvironment)
        {
            _logger = logger;
            _announceService = announceService;
            _articleService = articleService;
            _courseService = courseService;

            _hostEnvironment = hostEnvironment;

            yx.Add("beijingdaxue", "北京大学");
            yx.Add("qinghuadaxue", "清华大学");
            yx.Add("shjiaotongdaxue", "上海交通大学");
            yx.Add("zhongshandaxue", "中山大学");
            yx.Add("fudandaxue", "复旦大学");
            yx.Add("zgrenmindaxue", "中国人民大学");
            yx.Add("tongjidaxue", "同济大学");
            yx.Add("woaishangke", "我爱上课");

            kc.Add("zongheguanli", "綜合管理");
            kc.Add("gongsizhili", "公司治理");
            kc.Add("shichangyingxiao", "市场营销");
            kc.Add("jinrongyucaiwu", "金融与财务");
            kc.Add("hulianwang", "互联网+");
            kc.Add("chuangyechuangxin", "创业创新");
            kc.Add("yishurenwen", "艺术人文");
            kc.Add("youxue", "游学");
            kc.Add("qiyeneixun", "企业内训");

            qt.Add("jinqikaike", "近期开课");
            qt.Add("huodongyugao", "活动预告");
            qt.Add("jiaodianwenzhang", "焦点文章");
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetCourses(6);
            var hotCourses = await _courseService.GetHotCourses();
            var announces = await _announceService.GetAnnounces(6);
            var articles = await _articleService.GetArticles(42, 6);
            return View(new Tuple<IEnumerable<Course>, IEnumerable<Course>, IEnumerable<Article>, IEnumerable<Article>>(courses,hotCourses,announces,articles));
        }

        /// <summary>
        /// 定制课
        /// </summary>
        /// <returns></returns>
        [Route("/dingzhi")]
        public IActionResult Custom()
        {
            return View();
        }

        /// <summary>
        /// 线上学习
        /// </summary>
        /// <returns></returns>
        [Route("/online")]
        public IActionResult Online()
        {
            return View();
        }

        /// <summary>
        /// 公开课更多精彩
        /// </summary>
        /// <returns></returns>
        [Route("/jingcai")]
        public IActionResult Wonderful()
        {
            return View();
        }

        /// <summary>
        /// 专家校友
        /// </summary>
        /// <returns></returns>
        [Route("/zjxy")]
        public IActionResult ExpertsAndAlumni()
        {
            return View();
        }

        /// <summary>
        /// 课程
        /// </summary>
        /// <param name="kecheng"></param>
        /// <returns></returns>
        [Route("/kc/{kecheng}")]
        public async Task<IActionResult> Course(string kecheng)
        {
            ViewBag.KeCheng = kc[kecheng].ToString();
            var result = await _courseService.GetCoursesByCondtion("",kecheng);
            return View(result);
        }

        /// <summary>
        /// 院校
        /// </summary>
        /// <param name="yuanxiao"></param>
        /// <returns></returns>
        [Route("/yx/{yuanxiao}")]
        public async Task<IActionResult> Institution(string yuanxiao)
        {
            ViewBag.YuanXiao = yx[yuanxiao].ToString();
            var result = await _courseService.GetCoursesByCondtion(yuanxiao,"");
            return View(result);
        }

        /// <summary>
        /// 其它
        /// </summary>
        /// <param name="qita"></param>
        /// <returns></returns>
        [Route("/qt/{qita}")]
        public async Task<IActionResult> News(string qita)
        {
            ViewBag.Other = qt[qita].ToString();
            var result = qita switch
            {
                "jinqikaike" => (await _courseService.GetCoursesByCondtion(string.Empty, string.Empty)).ToArticles(),
                "huodongyugao" => await _announceService.GetAnnounces(0),
                _ => await _articleService.GetArticles(42),
            };
            return View(result);
        }

        /// <summary>
        /// 获取活动预告详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/announce/{id}")]
        public async Task<IActionResult> Announce(int id)
        {
            var result = await _announceService.GetAnnounce(id);
            return View("Content", result);
        }

        /// <summary>
        /// 获取新闻详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("/news/{id}")]
        public async Task<IActionResult> Content(int id)
        {
            var result = await _articleService.GetArticle(id, HttpContext.RemoteIp(), Request.BrowserNameByUserAgent(), Request.DeviceByUserAgent(), Request.OsByUserAgent());
            return View("Content", result);
        }

        /// <summary>
        /// 课程详情
        /// </summary>
        /// <param name="id">课程编号</param>
        /// <returns></returns>
        [Route("/detail/{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _courseService.GetCourse(id);
            return View(result);
        }

        /// <summary>
        /// 排行榜
        /// </summary>
        /// <returns></returns>
        [Route("/paihangbang")]
        public IActionResult Ranking()
        {
            return View();
        }
        
        [Route("/download")]
        public IActionResult Download(string fileName)
        {
            string rootPath = _hostEnvironment.ContentRootPath;           //E:\Workspace\Sd\Sd.5Ask.Web

            var stream = System.IO.File.OpenRead(rootPath + "/wwwroot/files/" + fileName);

            var fileExt = System.IO.Path.GetExtension(fileName);

            //获取文件的ContentType
            var provider = new FileExtensionContentTypeProvider();
            var memi = provider.Mappings[fileExt];

            return File(stream, memi, fileName);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
