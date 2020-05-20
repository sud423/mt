using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.Edu.Api.Models;

namespace Mt.Edu.Api.Infrastructure.EntityConfigurations
{
    public class ClaConfiguration : IEntityTypeConfiguration<Cla>
    {
        public void Configure(EntityTypeBuilder<Cla> builder)
        {
            builder.ToTable("cla");

            builder.HasKey(a => a.Id);
        }
    }
}
