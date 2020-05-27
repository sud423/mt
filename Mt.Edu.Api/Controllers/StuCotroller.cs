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
    public class StuCotroller : ControllerBase
    {
        private readonly EduDbContext _ctx;

        public StuCotroller(EduDbContext ctx)
        {
            _ctx = ctx;
        }

        public IActionResult GetInfos(string key,string channel,int page,int size)
        {
            var predicate = PredicateExtension.True<BasicInfo>();
            if (!string.IsNullOrEmpty(key))
            {
                predicate = predicate.And(a => a.Name.Contains(key) || a.Phone.Contains(key));
            }

            predicate = predicate.And(a => a.Channel == channel);

            var results= _ctx.BasicInfos.Where(predicate).ToPaged(page, size);

            return Ok(results);
        }

        public IActionResult GetInfos(string key, string channel)
        {
            var predicate = PredicateExtension.True<BasicInfo>();
            if (!string.IsNullOrEmpty(key))
            {
                predicate = predicate.And(a => a.Name.Contains(key) || a.Phone.Contains(key));
            }

            predicate = predicate.And(a => a.Channel == channel);

            var results = _ctx.BasicInfos.Where(predicate).Select(a => new { a.Name, Lable = a.Id + "-" + a.Name + " " + a.Phone });

            return Ok(results);
        }

        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _ctx.BasicInfos.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Create(BasicInfo basic)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());
            if (basic.Id > 0)
            {
                var old = _ctx.BasicInfos.FirstOrDefault(a => a.Id == basic.Id);
                old.Name = basic.Name;
                old.Sex = basic.Sex;
                old.Phone = basic.Phone;
                old.EMail = basic.EMail;
                old.Company = basic.Company;
                old.Fti = basic.Fti;
                old.Address = basic.Address;
                old.Remark = basic.Remark;
                _ctx.Update(old);
            }
            else
            {
                _ctx.Add(basic);
            }
           await _ctx.SaveChangesAsync();
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
            var basic = _ctx.BasicInfos.FirstOrDefault(a => a.Id == id);
            basic.Status = false;

            _ctx.Update(basic);
            _ctx.SaveChanges();
            return Ok();
        }

        public IActionResult GetInfoByClaId(int claId,string key,int page,int size)
        {
            var predicate = PredicateExtension.True<BasicInfo>();
            if (!string.IsNullOrEmpty(key))
            {
                predicate = predicate.And(a => a.Name.Contains(key) || a.Phone.Contains(key));
            }

            predicate = predicate.And(a => a.Stu.ClaId == claId);

            var results = _ctx.BasicInfos.Include(a=>a.Stu).Where(predicate).ToPaged(page, size);

            return Ok(results);
        }

        public IActionResult Quit(int stuId)
        {
            var stu = _ctx.Stus.FirstOrDefault(a=>a.Id==stuId);
            if (stu == null)
                return BadRequest(OptResult.Failed("学员不在该班级里"));

            stu.Status = false;
            _ctx.Update(stu);
            _ctx.SaveChanges();

            return Ok();
        }


    }
}
