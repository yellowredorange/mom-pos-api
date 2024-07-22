using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;
namespace MomPosApi.Services {
    public interface ICategoryService {
        Task<CategoryDto> AddAsync(CategoryDto dto);
        Task AddRangeAsync(IEnumerable<CategoryDto> dtos);

        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);

        Task<CategoryDto> UpdateAsync(CategoryDto dto);
        Task UpdateBatchAsync(IEnumerable<CategoryDto> categories);

        Task<bool> DeleteAsync(int id);
    }
}