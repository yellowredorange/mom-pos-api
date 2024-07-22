using MomPosApi.Models;

namespace MomPosApi.Services {
    public interface IMenuItemService {
        Task<MenuItemDto> AddAsync(MenuItemDto dto);
        Task AddRangeAsync(IEnumerable<MenuItemDto> dtos);

        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto> GetByIdAsync(int id);

        Task<MenuItemDto> UpdateAsync(MenuItemDto dto);
        Task UpdateBatchAsync(IEnumerable<MenuItemDto> menuItems);

        Task<bool> DeleteAsync(int id);
    }
}