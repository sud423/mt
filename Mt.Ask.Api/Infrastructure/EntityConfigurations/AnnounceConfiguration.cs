using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mt.Ask.Api.Models;

namespace Mt.Ask.Api.Infrastructure.EntityConfigurations
{
    public class AnnounceConfiguration : IEntityTypeConfiguration<Announce>
    {
        public void Configure(EntityTypeBuilder<Announce> builder)
        {
            builder.ToTable("announce");

            builder.HasKey(a => a.Id);
        }
    }
}
