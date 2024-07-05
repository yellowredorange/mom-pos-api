using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;

namespace MomPosApi.Services {
    public interface ICategoryService {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> AddAsync(CategoryDto dto);
        Task<CategoryDto> UpdateAsync(CategoryDto dto);
        Task<bool> DeleteAsync(int id);
    }


}