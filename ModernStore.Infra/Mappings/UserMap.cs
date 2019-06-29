using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Username).IsRequired().HasMaxLength(20).HasColumnType("varchar(20)");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(32).IsFixedLength();
            builder.Property(x => x.Active);
            builder.Ignore(x => x.Notifications);
        }
    }
}
