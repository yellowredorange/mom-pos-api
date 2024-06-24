using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Models;

namespace MomPosApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase {
        private readonly MomPosContext _context;
        public MenuController(MomPosContext context) {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Menu>>> GetMenu() {
            return await _context.Menu.ToListAsync();
        }
        [HttpPost]
        public async Task<IActionResult> PostMenu([FromBody] Menu menu) {
            if (!ModelState.IsValid) {
                return BadRequest(ModelState);
            }

            _context.Menu.Add(menu);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetMenu), new { id = menu.menu_id }, menu);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenu(int id) {
            var menu = await _context.Menu.FindAsync(id);

            if (menu == null) {
                return NotFound();
            }

            return Ok(menu);
        }
    }
}