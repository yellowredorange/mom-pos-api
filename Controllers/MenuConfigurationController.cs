using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MomPosApi.Models;
using MomPosApi.Services;
using Microsoft.Extensions.Logging;

namespace MomPosApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MenuConfigurationController : ControllerBase {
        private readonly IMenuConfigurationService _menuConfigurationService;
        private readonly ILogger<MenuConfigurationController> _logger;

        public MenuConfigurationController(IMenuConfigurationService menuConfigurationService, ILogger<MenuConfigurationController> logger) {
            _menuConfigurationService = menuConfigurationService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuConfigurationDto>>> GetAll() {
            try {
                var menuConfigurations = await _menuConfigurationService.GetAllAsync();
                return Ok(menuConfigurations);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting all menu configurations");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuConfigurationDto>> GetById(int id) {
            try {
                var menuConfiguration = await _menuConfigurationService.GetByIdAsync(id);
                if (menuConfiguration == null) {
                    return NotFound(new { Message = "Menu configuration not found" });
                }
                return Ok(menuConfiguration);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", id);
                return NotFound(new { Message = "Menu configuration not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting the menu configuration by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuConfigurationDto>> Add([FromBody] MenuConfigurationDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var createdMenuConfiguration = await _menuConfigurationService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMenuConfiguration.MenuConfigurationId }, createdMenuConfiguration);
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding a new menu configuration");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<MenuConfigurationDto> dtos) {
            if (dtos == null || !dtos.Any()) {
                return BadRequest(new { Message = "No entities provided" });
            }

            try {
                await _menuConfigurationService.AddRangeAsync(dtos);
                return Ok(new { Message = $"Successfully added {dtos.Count()} menu configurations", Count = dtos.Count() });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding menu configurations");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuConfigurationDto>> Update(int id, [FromBody] MenuConfigurationDto dto) {
            if (id != dto.MenuConfigurationId) {
                return BadRequest(new { Message = "Menu configuration ID mismatch" });
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var updatedMenuConfiguration = await _menuConfigurationService.UpdateAsync(dto);
                return Ok(updatedMenuConfiguration);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", dto.MenuConfigurationId);
                return NotFound(new { Message = "Menu configuration not found" });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating the menu configuration");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<MenuConfigurationDto> menuConfigurations) {
            if (menuConfigurations == null || !menuConfigurations.Any()) {
                return BadRequest(new { Message = "No menu configurations provided for update" });
            }

            try {
                await _menuConfigurationService.UpdateBatchAsync(menuConfigurations);
                return Ok(new { Message = $"Successfully updated {menuConfigurations.Count} menu configurations", Count = menuConfigurations.Count });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating menu configurations");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                await _menuConfigurationService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu configuration with id {MenuConfigurationId} not found", id);
                return NotFound(new { Message = "Menu configuration not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting the menu configuration");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("allmenus")]
        public async Task<ActionResult<IEnumerable<MenuConfigurationDto>>> GetMenus() {
            try {
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
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting all menus");
                return StatusCode(500, "Internal server error");
            }
        }
        [HttpPut("update-all")]
        public async Task<IActionResult> UpdateAll([FromBody] MenuConfigurationUpdateDto updateDto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                await _menuConfigurationService.UpdateAllAsync(updateDto);
                return Ok(new { Message = "All changes saved successfully" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating menu configuration");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}