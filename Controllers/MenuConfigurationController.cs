using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MomPosApi.Models;
using MomPosApi.Services;

namespace MomPosApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MenuConfigurationController : ControllerBase {
        private readonly IMenuConfigurationService _menuConfigurationService;

        public MenuConfigurationController(IMenuConfigurationService menuConfigurationService) {
            _menuConfigurationService = menuConfigurationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuConfigurationDto>>> GetAll() {
            var menuConfigurations = await _menuConfigurationService.GetAllAsync();
            return Ok(menuConfigurations);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuConfigurationDto>> GetById(int id) {
            var menuConfiguration = await _menuConfigurationService.GetByIdAsync(id);
            if (menuConfiguration == null) {
                return NotFound();
            }
            return Ok(menuConfiguration);
        }

        [HttpPost]
        public async Task<ActionResult<MenuConfigurationDto>> Add(MenuConfigurationDto dto) {
            var createdMenuConfiguration = await _menuConfigurationService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdMenuConfiguration.MenuConfigurationId }, createdMenuConfiguration);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuConfigurationDto dto) {
            if (id != dto.MenuConfigurationId) {
                return BadRequest();
            }

            var updatedMenuConfiguration = await _menuConfigurationService.UpdateAsync(dto);
            return Ok(updatedMenuConfiguration);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _menuConfigurationService.DeleteAsync(id);
            if (!result) {
                return NotFound();
            }

            return NoContent();
        }
        [HttpGet("allmenus")]
        public async Task<ActionResult<IEnumerable<MenuConfiguration>>> GetMenus() {
            var menuConfigurations = await _menuConfigurationService.GetAllMenusAsync();
            return Ok(menuConfigurations);
        }

    }

}