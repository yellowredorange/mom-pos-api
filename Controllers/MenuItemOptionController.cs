// MenuItemOptionController.cs
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
    public class MenuItemOptionController : ControllerBase {
        private readonly IMenuItemOptionService _menuItemOptionService;
        private readonly ILogger<MenuItemOptionController> _logger;

        public MenuItemOptionController(IMenuItemOptionService menuItemOptionService, ILogger<MenuItemOptionController> logger) {
            _menuItemOptionService = menuItemOptionService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemOptionDto>>> GetAll() {
            try {
                var menuItemOptions = await _menuItemOptionService.GetAllAsync();
                return Ok(menuItemOptions);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting all menu item options");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemOptionDto>> GetById(int id) {
            try {
                var menuItemOption = await _menuItemOptionService.GetByIdAsync(id);
                if (menuItemOption == null) {
                    return NotFound(new { Message = "Menu item option not found" });
                }
                return Ok(menuItemOption);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", id);
                return NotFound(new { Message = "Menu item option not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting the menu item option by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemOptionDto>> Add([FromBody] MenuItemOptionDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var createdMenuItemOption = await _menuItemOptionService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMenuItemOption.MenuItemOptionId }, createdMenuItemOption);
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding a new menu item option");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<MenuItemOptionDto> dtos) {
            if (dtos == null || !dtos.Any()) {
                return BadRequest(new { Message = "No entities provided" });
            }

            try {
                await _menuItemOptionService.AddRangeAsync(dtos);
                return Ok(new { Message = $"Successfully added {dtos.Count()} menu item options", Count = dtos.Count() });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding menu item options");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemOptionDto>> Update(int id, [FromBody] MenuItemOptionDto dto) {
            if (id != dto.MenuItemOptionId) {
                return BadRequest(new { Message = "Menu item option ID mismatch" });
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var updatedMenuItemOption = await _menuItemOptionService.UpdateAsync(dto);
                return Ok(updatedMenuItemOption);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", dto.MenuItemOptionId);
                return NotFound(new { Message = "Menu item option not found" });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating the menu item option");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<MenuItemOptionDto> menuItemOptions) {
            if (menuItemOptions == null || !menuItemOptions.Any()) {
                return BadRequest(new { Message = "No menu item options provided for update" });
            }

            try {
                await _menuItemOptionService.UpdateBatchAsync(menuItemOptions);
                return Ok(new { Message = $"Successfully updated {menuItemOptions.Count} menu item options", Count = menuItemOptions.Count });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating menu item options");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                await _menuItemOptionService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Menu item option with id {MenuItemOptionId} not found", id);
                return NotFound(new { Message = "Menu item option not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting the menu item option");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}