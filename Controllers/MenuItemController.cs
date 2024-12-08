// MenuItemController.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MomPosApi.Models;
using MomPosApi.Services;
using Microsoft.Extensions.Logging;

namespace MomPosApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;
        private readonly ILogger<MenuItemController> _logger;

        public MenuItemController(IMenuItemService menuItemService, ILogger<MenuItemController> logger)
        {
            _menuItemService = menuItemService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {
            try
            {
                var menuItems = await _menuItemService.GetAllAsync();
                return Ok(menuItems);
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting all menu items");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            try
            {
                var menuItem = await _menuItemService.GetByIdAsync(id);
                if (menuItem == null)
                {
                    return NotFound(new { Message = "Menu item not found" });
                }
                return Ok(menuItem);
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", id);
                return NotFound(new { Message = "Menu item not found" });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while getting the menu item by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Add([FromBody] MenuItemDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var createdMenuItem = await _menuItemService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.MenuItemId }, createdMenuItem);
            } catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new menu item");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<MenuItemDto> dtos)
        {
            if (dtos == null || !dtos.Any())
            {
                return BadRequest(new { Message = "No entities provided" });
            }

            try
            {
                await _menuItemService.AddRangeAsync(dtos);
                return Ok(new { Message = $"Successfully added {dtos.Count()} menu items", Count = dtos.Count() });
            } catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding menu items");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemDto>> Update(int id, [FromBody] MenuItemDto dto)
        {
            if (id != dto.MenuItemId)
            {
                return BadRequest(new { Message = "Menu item ID mismatch" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var updatedMenuItem = await _menuItemService.UpdateAsync(dto);
                return Ok(updatedMenuItem);
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", dto.MenuItemId);
                return NotFound(new { Message = "Menu item not found" });
            } catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating the menu item");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<MenuItemDto> menuItems)
        {
            if (menuItems == null || !menuItems.Any())
            {
                return BadRequest(new { Message = "No menu items provided for update" });
            }

            try
            {
                await _menuItemService.UpdateBatchAsync(menuItems);
                return Ok(new { Message = $"Successfully updated {menuItems.Count} menu items", Count = menuItems.Count });
            } catch (ArgumentException ex)
            {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while updating menu items");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _menuItemService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex)
            {
                _logger.LogWarning(ex, "Menu item with id {MenuItemId} not found", id);
                return NotFound(new { Message = "Menu item not found" });
            } catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while deleting the menu item");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}