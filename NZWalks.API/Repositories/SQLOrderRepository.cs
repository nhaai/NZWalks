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

        public async Task<List<Order>> GetAllAsync(string? filterOn = null, string? filterQuery = null,
            string? sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20)
        {
            var orders = dbContext.Orders.AsQueryable();

            // filtering
            if (!string.IsNullOrWhiteSpace(filterOn) && !string.IsNullOrWhiteSpace(filterQuery))
            {
                if (filterOn.Equals("Code", StringComparison.OrdinalIgnoreCase))
                {
                    orders = orders.Where(x => x.Code.Contains(filterQuery));
                }
            }

            // sorting
            if (!string.IsNullOrWhiteSpace(sortBy))
            {
                if (sortBy.Equals("CreatedAt", StringComparison.OrdinalIgnoreCase))
                {
                    orders = isAccending ? orders.OrderBy(x => x.CreatedAt) : orders.OrderByDescending(x => x.CreatedAt);
                }
            }

            // pagination
            var skipResults = (pageNumber - 1) * pageSize;
            orders = orders.Skip(skipResults).Take(pageSize);

            return await orders.ToListAsync();
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

            existingOrder.UserId = order.UserId;
            existingOrder.Code = order.Code;
            existingOrder.Total = order.Total;
            existingOrder.Status = order.Status;
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
