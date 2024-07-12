using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MomPosApi.Models;
using MomPosApi.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace MomPosApi.Services {
    public class MenuConfigurationService : IMenuConfigurationService {
        private readonly IMenuConfigurationRepository _menuConfigurationRepository;
        private readonly IMapper _mapper;

        public MenuConfigurationService(IMenuConfigurationRepository menuConfigurationRepository, IMapper mapper) {
            _menuConfigurationRepository = menuConfigurationRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MenuConfigurationDto>> GetAllAsync() {
            var menuConfigurations = await _menuConfigurationRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<MenuConfigurationDto>>(menuConfigurations);
        }

        public async Task<MenuConfigurationDto> GetByIdAsync(int id) {
            var menuConfiguration = await _menuConfigurationRepository.GetByIdAsync(id);
            return _mapper.Map<MenuConfigurationDto>(menuConfiguration);
        }

        public async Task<MenuConfigurationDto> AddAsync(MenuConfigurationDto dto) {
            var menuConfiguration = _mapper.Map<MenuConfiguration>(dto);
            var addedMenuConfiguration = await _menuConfigurationRepository.AddAsync(menuConfiguration);
            return _mapper.Map<MenuConfigurationDto>(addedMenuConfiguration);
        }

        public async Task<MenuConfigurationDto> UpdateAsync(MenuConfigurationDto dto) {
            var menuConfiguration = _mapper.Map<MenuConfiguration>(dto);
            var updatedMenuConfiguration = await _menuConfigurationRepository.UpdateAsync(menuConfiguration);
            return _mapper.Map<MenuConfigurationDto>(updatedMenuConfiguration);
        }

        public async Task<bool> DeleteAsync(int id) {
            return await _menuConfigurationRepository.DeleteAsync(id);
        }
        public async Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync() {
            return await _menuConfigurationRepository.GetAllMenusAsync();
        }

    }

}