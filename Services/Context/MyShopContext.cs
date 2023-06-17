using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyShop.Models.Entities;

namespace MyShop.Services.Context;

public class MyShopContext : IdentityDbContext<MyShopUser>
{
    public MyShopContext(DbContextOptions<MyShopContext> options)
        : base(options)
    {
    }
    public DbSet<ProductCategory> ProductCategory { get; set; }
    public DbSet<Product> ProductModel { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<ProductCategory>().ToTable("ProductCategory");
        builder.Entity<Product>().ToTable("ProductModel");
    }
}
