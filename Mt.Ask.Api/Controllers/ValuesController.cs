using Csp.EF.Extensions;
using Csp.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mt.Ask.Api.Infrastructure;
using Mt.Ask.Api.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Ask.Api.Controllers
{
    [Route("api/v1")]
    public class ValuesController : ControllerBase
    {

        private readonly AskDbContext _askDbContext;

        public ValuesController(AskDbContext askDbContext)
        {
            _askDbContext = askDbContext;
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet, Route("announces/{tenantId:int}")]
        public async Task<IActionResult> GetAnnounces(int tenantId,int? size)
        {
            var query = _askDbContext.Announces
                .Where(a => a.TenantId == tenantId && a.Status == 1 && (!a.EndTime.HasValue || a.EndTime.Value.CompareTo(DateTime.Now) > 0))
                .OrderBy(a => a.Sort).ThenByDescending(a => a.CreatedAt).AsQueryable();

            if (size != null && size.HasValue && size.GetValueOrDefault() > 0)
                query = query.Take(size.GetValueOrDefault());

            var result = await query.ToListAsync();
                
            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取通知信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("announces/find/{id:int}")]
        public async Task<IActionResult> GetAnnounceById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Announces.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <returns></returns>
        [HttpGet, Route("courses")]
        public async Task<IActionResult> GetCourses(int tenantId, string academy,string classify)
        {

            var predicate = PredicateExtension.True<Course>();

            predicate = predicate.And(a => a.TenantId == tenantId && a.Status == 1);

            if (!string.IsNullOrEmpty(academy))
            {
                predicate = predicate.And(a => a.Academy == academy);
            }

            if (!string.IsNullOrEmpty(classify))
            {
                predicate = predicate.And(a => a.Classify == classify);
            }


            var result = await _askDbContext.Courses
                .Where(predicate)
                .OrderBy(a => a.Sort)
                .ThenByDescending(a=>a.OpenDate)
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <param name="size"></param>
        /// <returns></returns>
        [HttpGet, Route("courses/{tenantId:int}/{size:int}")]
        public async Task<IActionResult> GetCourses(int tenantId, int size)
        {
            var result = await _askDbContext.Courses
                .Where(a => a.TenantId == tenantId && a.Status == 1)
                .OrderBy(a => a.Sort)
                .ThenByDescending(a => a.OpenDate)
                .Take(size)
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// 分页获取课程列表
        /// </summary>
        /// <param name="tenantId">所属租户编号</param>
        /// <returns></returns>
        [HttpGet, Route("courses/{tenantId:int}")]
        public async Task<IActionResult> GetHotCourse(int tenantId)
        {

            var result = await _askDbContext.Courses
                .Where(a => a.TenantId == tenantId && a.Status == 1 && a.IsHot)
                .OrderBy(a => a.Sort)
                .ThenBy(a=>a.OpenDate)
                .ToListAsync();

            return Ok(result);
        }

        /// <summary>
        /// 根据主键获取课程信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("courses/find/{id:int}")]
        public async Task<IActionResult> GetCourseById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _askDbContext.Courses.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }
    }
}
