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
    public class CategoryController : ControllerBase {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService) {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll() {
            var categories = await _categoryService.GetAllAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id) {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null) {
                return NotFound();
            }
            return Ok(category);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add(CategoryDto dto) {
            var createdCategory = await _categoryService.AddAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
        }
        [HttpPost("range")]
        public async Task<IActionResult> AddRange(IEnumerable<CategoryDto> dto) {
            if (dto == null || !dto.Any()) {
                return BadRequest("No entity provided");
            }
            try {
                await _categoryService.AddRangeAsync(dtos: dto);
                return Ok($"Successfully added {dto.Count()} entities");
            } catch (Exception) {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, CategoryDto dto) {
            if (id != dto.CategoryId) {
                return BadRequest();
            }

            var updatedCategory = await _categoryService.UpdateAsync(dto);
            return Ok(updatedCategory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            var result = await _categoryService.DeleteAsync(id);
            if (!result) {
                return NotFound();
            }

            return NoContent();
        }

    }

}