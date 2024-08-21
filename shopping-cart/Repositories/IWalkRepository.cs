using Microsoft.AspNetCore.Mvc;
using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace SA51_CA_Project_Team10.Repositories
{
    public interface IWalkRepository
    {
        Task<List<Walk>> GetAllAsync(string filterOn = null, string filterQuery = null,
            string sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20);
        Task<Walk?> GetByIdAsync(int id);
        Task<Walk> CreateAsync(Walk walk);
        Task<Walk?> UpdateAsync(int id, Walk walk);
        Task<Walk?> DeleteAsync(int id);
    }
}