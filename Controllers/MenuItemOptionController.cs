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
    public class MenuItemOptionController : ControllerBase {
        private readonly IMenuItemOptionService _menuItemOptionService;

        public MenuItemOptionController(IMenuItemOptionService menuItemOptionService) {
            _menuItemOptionService = menuItemOptionService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemOptionDto>>> GetAll() {
            var menuItemOptions = await _menuItemOptionService.GetAllAsync();
            return Ok(menuItemOptions);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemOptionDto>> GetById(int id) {
            var menuItemOption = await _menuItemOptionService.GetByIdAsync(id);
            if (menuItemOption == null) {
                return NotFound();
            }
            return Ok(menuItemOption);
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemOptionDto>> Add(MenuItemOptionDto dto) {
            var createdMenuItemOption = await _menuItemOptionService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdMenuItemOption.MenuItemOptionId }, createdMenuItemOption);
        }

        [HttpPost("range")]
        public async Task<IActionResult> AddRange(IEnumerable<MenuItemOptionDto> dto) {
            if (dto == null || !dto.Any()) {
                return BadRequest("No entity provided");
            }
            try {
                await _menuItemOptionService.AddRangeAsync(dto);
                return Ok($"Successfully added {dto.Count()} entities");
            } catch (Exception) {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, MenuItemOptionDto dto) {
            if (id != dto.MenuItemOptionId) {
                return BadRequest();
            }

            var updatedMenuItemOption = await _menuItemOptionService.UpdateAsync(dto);
            return Ok(updatedMenuItemOption);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _menuItemOptionService.DeleteAsync(id);
            if (!result) {
                return NotFound();
            }

            return NoContent();
        }
    }

}