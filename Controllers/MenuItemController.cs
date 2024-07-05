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