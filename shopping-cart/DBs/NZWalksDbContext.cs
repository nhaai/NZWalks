using Microsoft.EntityFrameworkCore;
using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;

namespace SA51_CA_Project_Team10
{
    public class NZWalksDbContext : DbContext
    {
        public NZWalksDbContext(DbContextOptions<NZWalksDbContext> dbContextOptions): base(dbContextOptions)
        {
        }

        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartLine> CartLines { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
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

            // Seed categories to DB
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Consoles",
                    IsActive = true
                },
                new Category
                {
                    Id = 2,
                    Name = "Games",
                    IsActive = true
                },
                new Category
                {
                    Id = 3,
                    Name = "Accessories",
                    IsActive = true
                },
                new Category
                {
                    Id = 4,
                    Name = "Cards",
                    IsActive = true
                },
                new Category
                {
                    Id = 5,
                    Name = "Sale",
                    IsActive = true
                }
            };

            modelBuilder.Entity<Category>().HasData(categories);
        }
    }
}
