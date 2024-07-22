using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MomPosApi.Models;
using Microsoft.Extensions.Logging;

namespace MomPosApi.Services {
    public class MenuItemOptionService : IMenuItemOptionService {
        private readonly IRepository<MenuItemOption> _menuItemOptionRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuItemOptionService> _logger;

        public MenuItemOptionService(IRepository<MenuItemOption> menuItemOptionRepository, IMapper mapper, ILogger<MenuItemOptionService> logger) {
            _menuItemOptionRepository = menuItemOptionRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MenuItemOptionDto>> GetAllAsync() {
            try {
                var menuItemOptions = await _menuItemOptionRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<MenuItemOptionDto>>(menuItemOptions);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving all menu item options");
                throw;
            }
        }

        public async Task<MenuItemOptionDto> GetByIdAsync(int id) {
            try {
                var menuItemOption = await _menuItemOptionRepository.GetByIdAsync(id);
                if (menuItemOption == null) {
                    _logger.LogWarning("Menu item option with id {MenuItemOptionId} not found", id);
                    throw new KeyNotFoundException($"Menu item option with id {id} not found");
                }
                return _mapper.Map<MenuItemOptionDto>(menuItemOption);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", id);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while retrieving the menu item option");
                throw;
            }
        }

        public async Task<MenuItemOptionDto> AddAsync(MenuItemOptionDto dto) {
            try {
                var menuItemOption = _mapper.Map<MenuItemOption>(dto);
                var addedMenuItemOption = await _menuItemOptionRepository.AddAsync(menuItemOption);
                return _mapper.Map<MenuItemOptionDto>(addedMenuItemOption);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding a new menu item option");
                throw;
            }
        }

        public async Task<MenuItemOptionDto> UpdateAsync(MenuItemOptionDto dto) {
            try {
                var menuItemOption = _mapper.Map<MenuItemOption>(dto);
                var updatedMenuItemOption = await _menuItemOptionRepository.UpdateAsync(menuItemOption);
                return _mapper.Map<MenuItemOptionDto>(updatedMenuItemOption);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", dto.MenuItemOptionId);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating the menu item option");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id) {
            try {
                var result = await _menuItemOptionRepository.DeleteAsync(id);
                if (!result) {
                    _logger.LogWarning("Menu item option with id {MenuItemOptionId} not found", id);
                    throw new KeyNotFoundException($"Menu item option with id {id} not found");
                }
                return result;
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", id);
                throw;
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting the menu item option");
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<MenuItemOptionDto> dtos) {
            try {
                var menuItemOptions = _mapper.Map<IEnumerable<MenuItemOption>>(dtos);
                await _menuItemOptionRepository.AddRangeAsync(menuItemOptions);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding multiple menu item options");
                throw;
            }
        }

        public async Task UpdateBatchAsync(IEnumerable<MenuItemOptionDto> menuItemOptionDtos) {
            try {
                var menuItemOptions = _mapper.Map<IEnumerable<MenuItemOption>>(menuItemOptionDtos);
                await _menuItemOptionRepository.UpdateBatchAsync(menuItemOptions);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating multiple menu item options");
                throw;
            }
        }
    }
}