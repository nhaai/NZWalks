using Microsoft.EntityFrameworkCore;
using SA51_CA_Project_Team10.DBs;
using SA51_CA_Project_Team10.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public class SQLRoleRepository : IRoleRepository
    {
        private readonly NZWalksAuthDbContext dbContext;

        public SQLRoleRepository(NZWalksAuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Role>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20)
        {
            var roles = dbContext.Roles.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    roles = roles.Where(x => x.Name.Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    roles = isAccending
                        ? roles.OrderBy(x => x.Name)
                        : roles.OrderByDescending(x => x.Name);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            roles = roles.Skip(skipResults).Take(pageSize);

            return await roles.ToListAsync();
        }

        public async Task<Role?> GetByIdAsync(string id)
        {
            return await dbContext.Roles.FindAsync(id);
        }

        public async Task<Role> CreateAsync(Role role)
        {
            await dbContext.Roles.AddAsync(role);
            await dbContext.SaveChangesAsync();

            return role;
        }

        public async Task<Role?> UpdateAsync(string id, Role role)
        {
            var existingRole = await dbContext.Roles.FindAsync(id);

            if (existingRole == null)
            {
                return null;
            }

            existingRole.Name = role.Name;
            existingRole.NormalizedName = role.NormalizedName;

            await dbContext.SaveChangesAsync();

            return existingRole;
        }

        public async Task<Role?> DeleteAsync(string id)
        {
            var existingRole = await dbContext.Roles.FindAsync(id);

            if (existingRole == null)
            {
                return null;
            }

            dbContext.Roles.Remove(existingRole);
            await dbContext.SaveChangesAsync();

            return existingRole;
        }
    }
}
