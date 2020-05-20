using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.Edu.Api.Models;

namespace Mt.Edu.Api.Infrastructure.EntityConfigurations
{
    public class TeaConfiguration : IEntityTypeConfiguration<Tea>
    {
        public void Configure(EntityTypeBuilder<Tea> builder)
        {
            builder.ToTable("tea");

            builder.HasKey(a => a.Id);
        }
    }
}
