using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MomPosApi.Models;
using MomPosApi.Repositories;

namespace MomPosApi.Services {
    public class MenuItemOptionService : IMenuItemOptionService {
        private readonly IRepository<MenuItemOption> _menuItemOptionRepository;
        private readonly IMapper _mapper;

        public MenuItemOptionService(IRepository<MenuItemOption> menuItemOptionRepository, IMapper mapper) {
            _menuItemOptionRepository = menuItemOptionRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuItemOptionDto>> GetAllAsync() {
            var menuItemOptions = await _menuItemOptionRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuItemOptionDto>>(menuItemOptions);
        }

        public async Task<MenuItemOptionDto> GetByIdAsync(int id) {
            var menuItemOption = await _menuItemOptionRepository.GetByIdAsync(id);
            return _mapper.Map<MenuItemOptionDto>(menuItemOption);
        }

        public async Task<MenuItemOptionDto> AddAsync(MenuItemOptionDto dto) {
            var menuItemOption = _mapper.Map<MenuItemOption>(dto);
            var addedMenuItemOption = await _menuItemOptionRepository.AddAsync(menuItemOption);
            return _mapper.Map<MenuItemOptionDto>(addedMenuItemOption);
        }

        public async Task<MenuItemOptionDto> UpdateAsync(MenuItemOptionDto dto) {
            var menuItemOption = _mapper.Map<MenuItemOption>(dto);
            var updatedMenuItemOption = await _menuItemOptionRepository.UpdateAsync(menuItemOption);
            return _mapper.Map<MenuItemOptionDto>(updatedMenuItemOption);
        }

        public async Task<bool> DeleteAsync(int id) {
            return await _menuItemOptionRepository.DeleteAsync(id);
        }
        public async Task AddRangeAsync(IEnumerable<MenuItemOptionDto> dtos) {
            IEnumerable<MenuItemOption> menuItemOptions = _mapper.Map<IEnumerable<MenuItemOption>>(source: dtos);
            await _menuItemOptionRepository.AddRangeAsync(menuItemOptions);
        }
    }

}