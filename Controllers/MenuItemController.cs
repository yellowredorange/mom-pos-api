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
    public class MenuItemController : ControllerBase {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService) {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll() {
            var menuItems = await _menuItemService.GetAllAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id) {
            var menuItem = await _menuItemService.GetByIdAsync(id);
            if (menuItem == null) {
                return NotFound();
            }
            return Ok(menuItem);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Add(MenuItemDto dto) {
            var createdMenuItem = await _menuItemService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdMenuItem.MenuItemId }, createdMenuItem);
        }
        [HttpPost("range")]
        public async Task<IActionResult> AddRange(IEnumerable<MenuItemDto> dto) {
            if (dto == null || !dto.Any()) {
                return BadRequest("No entity provided");
            }
            try {
                await _menuItemService.AddRangeAsync(dto);
                return Ok($"Successfully added {dto.Count()} entities");
            } catch (Exception) {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuItemDto dto) {
            if (id != dto.MenuItemId) {
                return BadRequest();
            }

            var updatedMenuItem = await _menuItemService.UpdateAsync(dto);
            return Ok(updatedMenuItem);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _menuItemService.DeleteAsync(id);
            if (!result) {
                return NotFound();
            }

            return NoContent();
        }
    }


}