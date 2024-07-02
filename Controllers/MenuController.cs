using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;
using System.Threading.Tasks;

namespace MomPosApi.Controllers {
    [ApiController]
    [Route("api/[controller]")]
    public class MenuController : ControllerBase {
        private readonly MomPosContext _context;

        public MenuController(MomPosContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetMenuItems() {
            var menuItems = await _context.MenuItems.ToListAsync();
            return Ok(menuItems);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetMenuItem(int id) {
            var menuItem = await _context.MenuItems.FindAsync(id);
            if (menuItem == null) {
                return NotFound();
            }
            return Ok(menuItem);
        }
    }
}
