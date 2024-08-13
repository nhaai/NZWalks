using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Data
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }

        // Add-Migration "InitialNZWalksDb" -Context "NZWalksDbContext"
        // Update-Database -Context "NZWalksDbContext"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(x => x.HasKey(ur => new { ur.UserId, ur.RoleId }));
            modelBuilder.Entity<Category>().HasOne(x => x.ParentCategory).WithMany(x => x.Children).HasForeignKey(x => x.ParentId).IsRequired(false);
        }
    }
}
