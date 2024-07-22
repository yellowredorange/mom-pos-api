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
    public class CategoryController : ControllerBase {
        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger) {
            _categoryService = categoryService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CategoryDto>>> GetAll() {
            try {
                var categories = await _categoryService.GetAllAsync();
                return Ok(categories);
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting all categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDto>> GetById(int id) {
            try {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null) {
                    return NotFound(new { Message = "Category not found" });
                }
                return Ok(category);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Category with id {CategoryId} not found", id);
                return NotFound(new { Message = "Category not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while getting the category by id");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDto>> Add([FromBody] CategoryDto dto) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var createdCategory = await _categoryService.AddAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdCategory.CategoryId }, createdCategory);
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding a new category");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost("batch")]
        public async Task<IActionResult> AddRange([FromBody] IEnumerable<CategoryDto> dtos) {
            if (dtos == null || !dtos.Any()) {
                return BadRequest(new { Message = "No entities provided" });
            }

            try {
                await _categoryService.AddRangeAsync(dtos);
                return Ok(new { Message = $"Successfully added {dtos.Count()} categories", Count = dtos.Count() });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while adding categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<CategoryDto>> Update(int id, [FromBody] CategoryDto dto) {
            if (id != dto.CategoryId) {
                return BadRequest(new { Message = "Category ID mismatch" });
            }

            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            try {
                var updatedCategory = await _categoryService.UpdateAsync(dto);
                return Ok(updatedCategory);
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Category with id {CategoryId} not found", dto.CategoryId);
                return NotFound(new { Message = "Category not found" });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating the category");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("batch")]
        public async Task<IActionResult> UpdateBatch([FromBody] List<CategoryDto> categories) {
            if (categories == null || !categories.Any()) {
                return BadRequest(new { Message = "No categories provided for update" });
            }

            try {
                await _categoryService.UpdateBatchAsync(categories);
                return Ok(new { Message = $"Successfully updated {categories.Count} categories", Count = categories.Count });
            } catch (ArgumentException ex) {
                _logger.LogWarning(ex, "Invalid argument provided");
                return BadRequest(new { Message = ex.Message });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while updating categories");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id) {
            try {
                await _categoryService.DeleteAsync(id);
                return NoContent();
            } catch (KeyNotFoundException ex) {
                _logger.LogWarning(ex, "Category with id {CategoryId} not found", id);
                return NotFound(new { Message = "Category not found" });
            } catch (Exception ex) {
                _logger.LogError(ex, "An error occurred while deleting the category");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}
