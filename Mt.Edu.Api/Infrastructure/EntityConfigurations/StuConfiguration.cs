using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.Edu.Api.Models;

namespace Mt.Edu.Api.Infrastructure.EntityConfigurations
{
    public class StuConfiguration : IEntityTypeConfiguration<Stu>
    {
        public void Configure(EntityTypeBuilder<Stu> builder)
        {
            builder.ToTable("stu");

            builder.HasKey(a => a.Id);

            builder.HasOne(a => a.BasicInfo).WithOne(b => b.Stu).HasForeignKey<Stu>(a => a.InfoId);
            builder.HasOne(a=>a.Cla).WithOne(a=>a.Stu).HasForeignKey<Stu>(a => a.ClaId);
        }
    }
}
