using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoicePro.Infrastructure.Data.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("customers");

        builder.HasKey(x => x.Id);

        builder.HasIndex(u => u.Email)
        .IsUnique();

        builder.Property(u => u.Name)
               .IsRequired();

        builder.Property(u => u.IsActive)
               .IsRequired();
    }
}