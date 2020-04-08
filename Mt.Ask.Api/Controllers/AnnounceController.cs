using Csp.EF.Paging;
using Csp.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mt.Ask.Api.Infrastructure;
using Mt.Ask.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class AnnounceController : ControllerBase
    {
        private readonly AskDbContext _askDbContext;

        public AnnounceController(AskDbContext askDbContext)
        {
            _askDbContext = askDbContext;
        }


        /// <summary>
        /// 分页获取通知列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet, Route("{tenantId:int}")]
        public async Task<IActionResult> Index(int tenantId, int page, int size)
        {
            var result = await _askDbContext.Announces
                .Where(a => a.TenantId == tenantId && a.Status == 1)
                .OrderBy(a => a.Sort)
                .ToPagedAsync(page, size);

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取通知信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Announces.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        /// <summary>
        /// 创建或更新通知
        /// </summary>
        /// <param name="article">通知信息</param>
        /// <returns></returns>
        public async Task<IActionResult> Create([FromBody]Announce announce)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            if (announce.Id > 0)
            {
                _askDbContext.Announces.Update(announce);
            }
            else
            {
                _askDbContext.Announces.Add(announce);
            }

            await _askDbContext.SaveChangesAsync();

            return Ok(OptResult.Success());
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">根据主键删除</param>
        /// <returns></returns>
        [HttpDelete, Route("delete/{id:int}")]
        public async Task<IActionResult> Deprecated(int id)
        {
            var course = await _askDbContext.Announces.SingleOrDefaultAsync(a => a.Id == id);

            if (course == null || course.Id <= 0)
                return BadRequest(OptResult.Failed("删除的数据不存在"));

            course.Status = 0;

            _askDbContext.Announces.Update(course);

            await _askDbContext.SaveChangesAsync();
            return Ok(OptResult.Success());

        }
    }
}
