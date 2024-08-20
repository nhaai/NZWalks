using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
namespace SA51_CA_Project_Team10.DBs
{
    public class NZWalksAuthDbContext : IdentityDbContext<
        User,
        Role,
        string,
        IdentityUserClaim<string>,
        UserRole,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,
        IdentityUserToken<string>>
    {
        public NZWalksAuthDbContext(DbContextOptions<NZWalksAuthDbContext> dbContextOptions) : base(dbContextOptions)
        {
        }

        // Add-Migration "InitialAuthDb" -Context "NZWalksAuthDbContext"
        // Update-Database -Context "NZWalksAuthDbContext"
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // https://github.com/dotnet/efcore/issues/24415#issuecomment-802248023
            modelBuilder.Entity<User>(x => x.HasMany(u => u.UserRoles).WithOne(u => u.User).HasForeignKey(ur => ur.UserId).IsRequired());
            modelBuilder.Entity<Role>(x => x.HasMany(r => r.UserRoles).WithOne(r => r.Role).HasForeignKey(ur => ur.RoleId).IsRequired());

            // Seed roles to DB
            var userRoleId = "f706c023-1472-42a9-ac2b-a68755a88c11";
            var adminRoleId = "0020292b-46fd-4333-b8bb-16e6e3ccc8d2";
            var ownerRoleId = "36272b3d-9461-402a-8a4d-12d9527fe8a6";

            var roles = new List<Role>
            {
                new Role
                {
                    Id = userRoleId,
                    ConcurrencyStamp = userRoleId,
                    Name = "user",
                    NormalizedName = "USER",
                },
                new Role
                {
                    Id = adminRoleId,
                    ConcurrencyStamp = adminRoleId,
                    Name = "admin",
                    NormalizedName = "ADMIN",
                },
                new Role
                {
                    Id = ownerRoleId,
                    ConcurrencyStamp = ownerRoleId,
                    Name = "owner",
                    NormalizedName = "OWNER",
                },
            };

            modelBuilder.Entity<Role>().HasData(roles);
        }
    }
}
