using InvoicePro.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InvoicePro.Infrastructure;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}

    public DbSet<User> Users {get; set;}
    public DbSet<Organization> Organizations {get; set;}

    // protected override void OodelCreating(ModelBuilder modelBuilder)
    // {
        

    //     base.OnModelCreating(modelBuilder);
    // }
}