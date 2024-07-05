using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;

namespace MomPosApi.Services {
    public interface IMenuItemOptionService {
        Task<IEnumerable<MenuItemOptionDto>> GetAllAsync();
        Task<MenuItemOptionDto> GetByIdAsync(int id);
        Task<MenuItemOptionDto> AddAsync(MenuItemOptionDto dto);
        Task<MenuItemOptionDto> UpdateAsync(MenuItemOptionDto dto);
        Task<bool> DeleteAsync(int id);
    }

}