using MomPosApi.Models;

namespace MomPosApi.Services
{
    public interface IMenuItemOptionService
    {
        Task<MenuItemOptionDto> AddAsync(MenuItemOptionDto dto);
        Task AddRangeAsync(IEnumerable<MenuItemOptionDto> dtos);

        Task<IEnumerable<MenuItemOptionDto>> GetAllAsync();
        Task<MenuItemOptionDto> GetByIdAsync(int id);

        Task<MenuItemOptionDto> UpdateAsync(MenuItemOptionDto dto);
        Task UpdateBatchAsync(IEnumerable<MenuItemOptionDto> menuItemOptions);

        Task<bool> DeleteAsync(int id);
    }

}