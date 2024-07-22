using MomPosApi.Models;

public interface IMenuConfigurationService {
    Task<MenuConfigurationDto> AddAsync(MenuConfigurationDto dto);
    Task AddRangeAsync(IEnumerable<MenuConfigurationDto> dtos);

    Task<IEnumerable<MenuConfigurationDto>> GetAllAsync();
    Task<MenuConfigurationDto> GetByIdAsync(int id);
    Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync();

    Task<MenuConfigurationDto> UpdateAsync(MenuConfigurationDto dto);
    Task UpdateBatchAsync(IEnumerable<MenuConfigurationDto> menuConfigurations);

    Task<bool> DeleteAsync(int id);
}

