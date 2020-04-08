using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mt.Fruit.Web.Services;
using System.Drawing;
using System.Threading.Tasks;

namespace Mt.Fruit.Web.Controllers
{
    public class OssController : Controller
    {
        private readonly IOssService _ossService;

        public OssController(IOssService ossService)
        {
            _ossService = ossService;
        }

        [HttpPost]
        [DisableRequestSizeLimit]
        public async Task<IActionResult> Upload(IFormFile file,string dir="image")
        {
            //var file = Request.Form.Files[0];

            //文件类型
            //string dirName = Request.Query["dir"];

            if (dir == "image")
            {
                //处理照片大小
                using var image = Image.FromStream(file.OpenReadStream());
                if (image.Width > 1024 || image.Height > 720)
                {
                    return Ok(new { Error = 1, Message = "图片太大，最佳图片宽高为：1024*720" });
                }

            }

            var result = await _ossService.Upload(file, dir);

            return Ok(new { Error = (result.Succeed ? 0 : 1), message = result.Msg, Url = result.Msg });
        }
    }
}
