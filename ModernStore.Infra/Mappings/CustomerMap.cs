using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ModernStore.Domain.Entities;

namespace ModernStore.Infra.Mappings
{
    public class CustomerMap : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.ToTable("Customer");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.BirthDate);
            builder.OwnsOne(x => x.Document, document =>
            {
                document.Property(x => x.Number)
                .IsRequired().HasMaxLength(11)
                .IsFixedLength().HasColumnType("varchar(11)")
                .HasColumnName("DocumentNumber");

                document.Ignore(x => x.Notifications);
            });
            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(x => x.Address)
                .IsRequired()
                .HasMaxLength(150)
                .HasColumnType("varchar(150)")
                .HasColumnName("Email");
                email.Ignore(x => x.Notifications);
            });
            builder.OwnsOne(x => x.Name, name =>
            {
                name.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("varchar(50)")
                .HasColumnName("FirstName");

                name.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(80)
                .HasColumnType("varchar(80)")
                .HasColumnName("LastName");
                name.Ignore(x => x.Notifications);
            });
            builder.HasOne(x => x.User);
            builder.Ignore(x => x.Notifications);
        }
    }
}
