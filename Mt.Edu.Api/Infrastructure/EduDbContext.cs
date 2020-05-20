using Csp.EF;
using Microsoft.EntityFrameworkCore;
using Mt.Edu.Api.Models;

namespace Mt.Edu.Api.Infrastructure
{
    public class EduDbContext : CspDbContext<EduDbContext>
    {
        public EduDbContext(DbContextOptions<EduDbContext> options) : base(options)
        {
        }

        public DbSet<Cla> Clas { get; set; }

        public DbSet<BasicInfo> BasicInfos { get; set; }

        public DbSet<Stu> Stus { get; set; }

        public DbSet<Tea> Teas { get; set; }
    }
}
