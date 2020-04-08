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
    public class CourseController : ControllerBase
    {
        private readonly AskDbContext _askDbContext;

        public CourseController(AskDbContext askDbContext)
        {
            _askDbContext = askDbContext;
        }


        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="page"></param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet,Route("{tenantId:int}")]
        public async Task<IActionResult> Index(int tenantId,int page,int size)
        {
            var result = await _askDbContext.Courses
                .Where(a => a.TenantId == tenantId && a.Status == 1)
                .OrderBy(a => a.Sort)
                .ToPagedAsync(page, size);

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取课程信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Courses.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        /// <summary>
        /// 创建或更新课程
        /// </summary>
        /// <param name="article">课程信息</param>
        /// <returns></returns>
        public async Task<IActionResult> Create([FromBody]Course course)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());

            if (course.Id > 0)
            {
                _askDbContext.Courses.Update(course);
            }
            else
            {
                _askDbContext.Courses.Add(course);
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
            var course = await _askDbContext.Courses.SingleOrDefaultAsync(a => a.Id == id);

            if (course == null || course.Id <= 0)
                return BadRequest(OptResult.Failed("删除的数据不存在"));

            course.Status = 0;

            _askDbContext.Courses.Update(course);

            await _askDbContext.SaveChangesAsync();
            return Ok(OptResult.Success());

        }
    }
}
