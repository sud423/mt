using Csp.EF;
using Microsoft.EntityFrameworkCore;
using Mt.Ask.Api.Models;

namespace Mt.Ask.Api.Infrastructure
{
    public class AskDbContext : CspDbContext<AskDbContext>
    {
        public AskDbContext(DbContextOptions<AskDbContext> options) : base(options)
        {
        }

        public DbSet<Announce> Announces { get; set; }

        public DbSet<Course> Courses { get; set; }
    }
}
