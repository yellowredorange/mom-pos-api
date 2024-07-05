using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;

namespace MomPosApi.Services {
    public interface IMenuItemService {
        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto> GetByIdAsync(int id);
        Task<MenuItemDto> AddAsync(MenuItemDto dto);
        Task<MenuItemDto> UpdateAsync(MenuItemDto dto);
        Task<bool> DeleteAsync(int id);
    }
}