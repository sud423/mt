using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.Edu.Api.Models;

namespace Mt.Edu.Api.Infrastructure.EntityConfigurations
{
    public class BasicInfoConfiguration : IEntityTypeConfiguration<BasicInfo>
    {
        public void Configure(EntityTypeBuilder<BasicInfo> builder)
        {
            builder.ToTable("basicinfo");

            builder.HasKey(a => a.Id);
        }
    }
}
