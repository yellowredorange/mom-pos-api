using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MomPosApi.Models;

namespace MomPosApi.Services {
    public class MenuItemService : IMenuItemService {
        private readonly IRepository<MenuItem> _menuItemRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IRepository<MenuItem> menuItemRepository, IMapper mapper) {
            _menuItemRepository = menuItemRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync() {
            var menuItems = await _menuItemRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuItemDto>>(menuItems);
        }

        public async Task<MenuItemDto> GetByIdAsync(int id) {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            return _mapper.Map<MenuItemDto>(menuItem);
        }

        public async Task<MenuItemDto> AddAsync(MenuItemDto dto) {
            var menuItem = _mapper.Map<MenuItem>(dto);
            var addedMenuItem = await _menuItemRepository.AddAsync(menuItem);
            return _mapper.Map<MenuItemDto>(addedMenuItem);
        }

        public async Task<MenuItemDto> UpdateAsync(MenuItemDto dto) {
            var menuItem = _mapper.Map<MenuItem>(dto);
            var updatedMenuItem = await _menuItemRepository.UpdateAsync(menuItem);
            return _mapper.Map<MenuItemDto>(updatedMenuItem);
        }

        public async Task<bool> DeleteAsync(int id) {
            return await _menuItemRepository.DeleteAsync(id);
        }

        public async Task AddRangeAsync(IEnumerable<MenuItemDto> dtos) {
            IEnumerable<MenuItem> menuItems = _mapper.Map<IEnumerable<MenuItem>>(dtos);
            await _menuItemRepository.AddRangeAsync(menuItems);
        }
    }
}