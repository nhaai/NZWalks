using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;
using NZWalks.API.Models.DTO;
using System.Linq;

namespace NZWalks.API.Repositories
{
    public class SQLUserRepository : IUserRepository
    {
        private readonly NZWalksAuthDbContext dbContext;

        public SQLUserRepository(NZWalksAuthDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<User>, int)> GetAllAsync(SearchFilter searchFilter)
        {
            var users = dbContext.Users.Include("UserRoles.Role").AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(searchFilter.FilterOn) && !string.IsNullOrWhiteSpace(searchFilter.FilterQuery))
            {
                if (searchFilter.FilterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    users = users.Where(x => x.LastName.Contains(searchFilter.FilterQuery) || x.FirstName.Contains(searchFilter.FilterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(searchFilter.SortBy))
            {
                if (searchFilter.SortBy.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    users = searchFilter.IsAccending == true
                        ? users.OrderBy(x => x.LastName).ThenBy(x => x.FirstName)
                        : users.OrderByDescending(x => x.LastName).ThenByDescending(x => x.FirstName);
                }
            }

            var count = await users.CountAsync();

            // pagination
            if (searchFilter.PageNumber != null)
            {
                var skipResults = ((int)searchFilter.PageNumber - 1) * searchFilter.PageSize;
                users = users.Skip(skipResults).Take(searchFilter.PageSize);
            }

            return (await users.ToListAsync(), count);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await dbContext.Users.FindAsync(id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateAsync(string id, User user)
        {
            var existingUser = await dbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            existingUser.Title = user.Title;
            existingUser.FirstName = user.FirstName;
            existingUser.LastName = user.LastName;
            existingUser.AddressLine1 = user.AddressLine1;
            existingUser.AddressLine2 = user.AddressLine2;
            existingUser.City = user.City;
            existingUser.PostalCode = user.PostalCode;
            existingUser.Country = user.Country;

            await dbContext.SaveChangesAsync();

            return existingUser;
        }

        public async Task<User?> DeleteAsync(string id)
        {
            var existingUser = await dbContext.Users.FindAsync(id);

            if (existingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();

            return existingUser;
        }
    }
}
