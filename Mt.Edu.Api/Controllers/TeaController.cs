using Csp.EF.Extensions;
using Csp.EF.Paging;
using Csp.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mt.Edu.Api.Infrastructure;
using Mt.Edu.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Edu.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class TeaController : ControllerBase
    {
        private readonly EduDbContext _ctx;

        public TeaController(EduDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult GetAll(string name,string phone,string type,string channel,string course,int page,int size)
        {
            var predicate = PredicateExtension.True<Tea>();
            predicate = predicate.And(a => a.Channel == channel);
            if (!string.IsNullOrEmpty(name))
                predicate = predicate.And(a => a.Name.Contains(name));
            if (!string.IsNullOrEmpty(phone))
                predicate = predicate.And(a => a.Phone.Contains(phone));
            if (!string.IsNullOrEmpty(type))
                predicate = predicate.And(a => a.Type.Contains(type));
            if (!string.IsNullOrEmpty(course))
                predicate = predicate.And(a => a.Courses.Contains(course));

            var results = _ctx.Teas.Where(predicate).ToPaged(page,size);

            return Ok(results);

        }

        /// <summary>
        /// 根据主键获取师资信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _ctx.Teas.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Create(Tea tea)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());
            if (tea.Id > 0)
            {
                var old = _ctx.Teas.FirstOrDefault(a => a.Id == tea.Id);
                old.Name = tea.Name;
                old.Sex = tea.Sex;
                old.Phone = tea.Phone;
                old.EMail = tea.EMail;
                old.Type = tea.Type;
                old.Profiles = tea.Profiles;
                old.Courses = tea.Courses;
                old.FixedPhone = tea.FixedPhone;
                old.Address = tea.Address;
                old.Score = tea.Score;
                old.Remark = tea.Remark;
                _ctx.Update(old);
            }
            else
            {
                _ctx.Add(tea);
            }
            _ctx.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// 根据主键编号删除
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var tea = _ctx.Teas.FirstOrDefault(a => a.Id == id);
            tea.Status = false;

            _ctx.Update(tea);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
