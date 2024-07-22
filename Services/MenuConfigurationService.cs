using AutoMapper;
using MomPosApi.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MomPosApi.Services {
    public class MenuConfigurationService : IMenuConfigurationService {
        private readonly IMenuConfigurationRepository _menuConfigurationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuConfigurationService> _logger;

        public MenuConfigurationService(IMenuConfigurationRepository menuConfigurationRepository, IMapper mapper, ILogger<MenuConfigurationService> logger) {
            _menuConfigurationRepository = menuConfigurationRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MenuConfigurationDto>> GetAllAsync() {
            try {
                var menuConfigurations = await _menuConfigurationRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<MenuConfigurationDto>>(menuConfigurations);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving all menu configurations");
                throw;
            }
        }

        public async Task<MenuConfigurationDto> GetByIdAsync(int id) {
            try {
                var menuConfiguration = await _menuConfigurationRepository.GetByIdAsync(id);
                if (menuConfiguration == null) {
                    _logger.LogWarning("Menu configuration with id {MenuConfigurationId} not found", id);
                    throw new KeyNotFoundException($"Menu configuration with id {id} not found");
                }
                return _mapper.Map<MenuConfigurationDto>(menuConfiguration);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", id);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving the menu configuration");
                throw;
            }
        }

        public async Task<MenuConfigurationDto> AddAsync(MenuConfigurationDto dto) {
            try {
                var menuConfiguration = _mapper.Map<MenuConfiguration>(dto);
                var addedMenuConfiguration = await _menuConfigurationRepository.AddAsync(menuConfiguration);
                return _mapper.Map<MenuConfigurationDto>(addedMenuConfiguration);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding a new menu configuration");
                throw;
            }
        }

        public async Task<MenuConfigurationDto> UpdateAsync(MenuConfigurationDto dto) {
            try {
                var menuConfiguration = _mapper.Map<MenuConfiguration>(dto);
                var updatedMenuConfiguration = await _menuConfigurationRepository.UpdateAsync(menuConfiguration);
                return _mapper.Map<MenuConfigurationDto>(updatedMenuConfiguration);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", dto.MenuConfigurationId);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating the menu configuration");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            try {
                var result = await _menuConfigurationRepository.DeleteAsync(id);
                if (!result) {
                    _logger.LogWarning("Menu configuration with id {MenuConfigurationId} not found", id);
                    throw new KeyNotFoundException($"Menu configuration with id {id} not found");
                }
                return result;
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", id);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting the menu configuration");
                throw;
            }
        }

        public async Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync() {
            try {
                return await _menuConfigurationRepository.GetAllMenusAsync();
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving all menus");
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<MenuConfigurationDto> dtos) {
            try {
                var menuConfigurations = _mapper.Map<IEnumerable<MenuConfiguration>>(dtos);
                await _menuConfigurationRepository.AddRangeAsync(menuConfigurations);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding multiple menu configurations");
                throw;
            }
        }

        public async Task UpdateBatchAsync(IEnumerable<MenuConfigurationDto> menuConfigurationDtos) {
            try {
                var menuConfigurations = _mapper.Map<IEnumerable<MenuConfiguration>>(menuConfigurationDtos);
                await _menuConfigurationRepository.UpdateBatchAsync(menuConfigurations);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating multiple menu configurations");
                throw;
            }
        }
    }
}