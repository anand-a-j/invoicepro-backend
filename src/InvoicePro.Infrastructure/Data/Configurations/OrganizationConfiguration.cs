using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicePro.Infrastructure.Data.Configurations;

public class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.ToTable("organizations");

        builder.HasKey(x => x.Id);

        builder.HasIndex(u => u.Email)
        .IsUnique();

        builder.Property(u => u.Name)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .IsRequired();
    }
}