using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using MomPosApi.Models;
using Microsoft.Extensions.Logging;

namespace MomPosApi.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<MenuItemService> _logger;

        public MenuItemService(IRepository<MenuItem> menuItemRepository, IMapper mapper, ILogger<MenuItemService> logger)
        {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
        {
            try
            {
                var menuItems = await _menuItemRepository.GetAllAsync();
                return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving all menu items");
                throw;
            }
        }

        public async Task<MenuItemDto> GetByIdAsync(int id)
        {
            try
            {
                var menuItem = await _menuItemRepository.GetByIdAsync(id);
                if (menuItem == null)
                {
                    _logger.LogWarning("Menu item with id {MenuItemId} not found", id);
                    throw new KeyNotFoundException($"Menu item with id {id} not found");
                }
                return _mapper.Map<MenuItemDto>(menuItem);
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", id);
                throw;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while retrieving the menu item");
                throw;
            }
        }

        public async Task<MenuItemDto> AddAsync(MenuItemDto dto)
        {
            try
            {
                var menuItem = _mapper.Map<MenuItem>(dto);
                var addedMenuItem = await _menuItemRepository.AddAsync(menuItem);
                return _mapper.Map<MenuItemDto>(addedMenuItem);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new menu item");
                throw;
            }
        }

        public async Task<MenuItemDto> UpdateAsync(MenuItemDto dto)
        {
            try
            {
                var menuItem = _mapper.Map<MenuItem>(dto);
                var updatedMenuItem = await _menuItemRepository.UpdateAsync(menuItem);
                return _mapper.Map<MenuItemDto>(updatedMenuItem);
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", dto.MenuItemId);
                throw;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the menu item");
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var result = await _menuItemRepository.DeleteAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Menu item with id {MenuItemId} not found", id);
                    throw new KeyNotFoundException($"Menu item with id {id} not found");
                }
                return result;
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", id);
                throw;
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the menu item");
                throw;
            }
        }

        public async Task AddRangeAsync(IEnumerable<MenuItemDto> dtos)
        {
            try
            {
                var menuItems = _mapper.Map<IEnumerable<MenuItem>>(dtos);
                await _menuItemRepository.AddRangeAsync(menuItems);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding multiple menu items");
                throw;
            }
        }

        public async Task UpdateBatchAsync(IEnumerable<MenuItemDto> menuItemDtos)
        {
            try
            {
                var menuItems = _mapper.Map<IEnumerable<MenuItem>>(menuItemDtos);
                await _menuItemRepository.UpdateBatchAsync(menuItems);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating multiple menu items");
                throw;
            }
        }
    }
}