using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
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
        [HttpPost("range")]
        public async Task<IActionResult> AddRange(IEnumerable<MenuConfigurationDto> dto) {
            if (dto == null || !dto.Any()) {
                return BadRequest("No entity provided");
            }
            try {
                await _menuConfigurationService.AddRangeAsync(dto);
                return Ok($"Successfully added {dto.Count()} entities");
            } catch (Exception) {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        [HttpGet("allmenus")]
        public async Task<ActionResult<IEnumerable<MenuConfigurationDto>>> GetMenus() {
            var menuConfigurations = await _menuConfigurationService.GetAllMenusAsync();
            var result = menuConfigurations.Select(mc => new {
                mc.MenuConfigurationId,
                mc.Name,
                mc.IsActive,
                mc.SortOrder,
                Categories = mc.Categories.Select(c => new {
                    c.CategoryId,
                    c.Name,
                    c.IsActive,
                    c.SortOrder,
                    MenuItems = c.MenuItems.Select(mi => new {
                        mi.MenuItemId,
                        mi.Name,
                        mi.Description,
                        mi.Price,
                        mi.IsActive,
                        mi.PhotoUrl,
                        mi.SortOrder,
                        MenuItemOptions = mi.MenuItemOptions.Select(mio => new {
                            mio.MenuItemOptionId,
                            mio.Option,
                            mio.OptionCategory,
                            mio.AdditionalPrice,
                            mio.SortOrder
                        })
                    })
                })
            });
            return Ok(result);
        }
    }
}