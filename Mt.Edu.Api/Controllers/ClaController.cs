using Csp.EF.Extensions;
using Csp.EF.Paging;
using Csp.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mt.Edu.Api.Factories;
using Mt.Edu.Api.Infrastructure;
using Mt.Edu.Api.Models;
using System.Linq;
using System.Threading.Tasks;

namespace Mt.Edu.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    public class ClaController : ControllerBase
    {
        private readonly EduDbContext _ctx;

        public ClaController(EduDbContext ctx)
        {
            _ctx = ctx;
        }

        /// <summary>
        /// 获取班级树
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetClaTree(string channel)
        {
            var clas = await _ctx.Clas.Where(a=>a.Status && a.Channel==channel).ToListAsync();
            var roots = clas.Where(a => a.ParentId == 0).ToTrees().ToList();

            for(int i = 0; i < roots.Count; i++)
            {
                var result = clas.Where(a => a.ParentId == roots[i].Id).ToTrees().ToList();
                roots[i].Children = result;
            }

            return Ok(roots);
        }

        /// <summary>
        /// 获取全部的班级或项目
        /// </summary>
        /// <param name="channel">渠道 training：培训 basic：基础教育</param>
        /// <param name="isCla">是否查班级</param>
        /// <param name="name">名称</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll(string channel, bool isCla,int page,int size,string name="")
        {
            var predicate = PredicateExtension.True<Cla>();
            predicate = predicate.And(a => a.Channel == channel).And(a => isCla ? a.ParentId != 0 : a.ParentId == 0);
            if (!string.IsNullOrEmpty(name))
                predicate = predicate.And(a => a.Name.Contains(name));

            var clas = await _ctx.Clas.Where(predicate).ToPagedAsync(page, size);

            return Ok(clas);
        }

        /// <summary>
        /// 根据主键获取班级信息
        /// </summary>
        /// <param name="id">主键编号</param>
        /// <returns></returns>
        [HttpGet, Route("find/{id:int}")]
        public async Task<IActionResult> FindById(int id)
        {
            if (id == 0)
                return BadRequest(OptResult.Failed("id不能小于或为0"));

            var result = await _ctx.Clas.SingleOrDefaultAsync(a => a.Id == id);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Cla cla)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState.First());
            if (cla.Id > 0)
            {
                var old = _ctx.Clas.FirstOrDefault(a=>a.Id==cla.Id);
                old.Name = cla.Name;
                old.ParentId = cla.ParentId;
                old.Summary = cla.Summary;
                old.Remark = cla.Remark;
                _ctx.Update(old);
            }
            else
            {
                _ctx.Add(cla);
            }
            _ctx.SaveChanges();
            return Ok();
        }

        /// <summary>
        /// 根据主键编号删除
        /// </summary>
        /// <param name="id">项目 或班级信息</param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var cla = _ctx.Clas.FirstOrDefault(a=>a.Id==id);
            cla.Status = false;

            _ctx.Update(cla);
            _ctx.SaveChanges();
            return Ok();
        }
    }
}
