﻿using SA51_CA_Project_Team10.Models.Domain;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SA51_CA_Project_Team10.Repositories
{
    public interface IRoleRepository
    {
        Task<List<Role>> GetAllAsync(string filterOn = null, string filterQuery = null,
            string sortBy = null, bool isAccending = true, int pageNumber = 1, int pageSize = 20);
        Task<Role?> GetByIdAsync(string id);
        Task<Role> CreateAsync(Role role);
        Task<Role?> UpdateAsync(string id, Role role);
        Task<Role?> DeleteAsync(string id);
    }
}
