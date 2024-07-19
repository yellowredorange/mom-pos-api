using MomPosApi.Models;

public interface IMenuConfigurationService {
    Task<IEnumerable<MenuConfigurationDto>> GetAllAsync();
    Task<MenuConfigurationDto> GetByIdAsync(int id);
    Task<MenuConfigurationDto> AddAsync(MenuConfigurationDto dto);
    Task<MenuConfigurationDto> UpdateAsync(MenuConfigurationDto dto);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync();
    Task AddRangeAsync(IEnumerable<MenuConfigurationDto> dtos);
}

