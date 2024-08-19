using Microsoft.EntityFrameworkCore;
using NZWalks.API.Data;
using NZWalks.API.Models.Domain;

namespace NZWalks.API.Repositories
{
    public class SQLOrderRepository : IOrderRepository
    {
        private readonly NZWalksDbContext dbContext;

        public SQLOrderRepository(NZWalksDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<(List<Order>, int)> GetAllAsync(SearchFilter searchFilter)
        {
            var orders = dbContext.Orders.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(searchFilter.FilterOn) && !string.IsNullOrWhiteSpace(searchFilter.FilterQuery))
            {
                if (searchFilter.FilterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    orders = orders.Where(x => x.Code.Contains(searchFilter.FilterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(searchFilter.SortBy))
            {
                if (searchFilter.SortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    orders = searchFilter.IsAccending == true
                        ? orders.OrderBy(x => x.CreatedAt)
                        : orders.OrderByDescending(x => x.CreatedAt);
                }
            }

            var totalItemCount = await orders.CountAsync();

            // pagination
            if (searchFilter.PageNumber != null)
            {
                var skipResults = ((int)searchFilter.PageNumber - 1) * searchFilter.PageSize;
                orders = orders.Skip(skipResults).Take(searchFilter.PageSize);
            }

            return (await orders.ToListAsync(), totalItemCount);
        }

        public async Task<Order?> GetByIdAsync(int id)
        {
            return await dbContext.Orders.FindAsync(id);
        }

        public async Task<Order> CreateAsync(Order order)
        {
            await dbContext.Orders.AddAsync(order);
            await dbContext.SaveChangesAsync();

            return order;
        }

        public async Task<Order?> UpdateAsync(int id, Order order)
        {
            var existingOrder = await dbContext.Orders.FindAsync(id);

            if (existingOrder == null)
            {
                return null;
            }

            existingOrder.GrandTotal = order.GrandTotal;
            existingOrder.UpdatedAt = order.UpdatedAt;

            await dbContext.SaveChangesAsync();

            return existingOrder;
        }

        public async Task<Order?> DeleteAsync(int id)
        {
            var existingOrder = await dbContext.Orders.FindAsync(id);

            if (existingOrder == null)
            {
                return null;
            }

            dbContext.Orders.Remove(existingOrder);
            await dbContext.SaveChangesAsync();

            return existingOrder;
        }
    }
}
