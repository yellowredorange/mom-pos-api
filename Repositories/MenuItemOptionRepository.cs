using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

namespace MomPosApi.Repositories {
    public class MenuItemOptionRepository : IMenuItemOptionRepository {
        private readonly MomPosContext _context;

        public MenuItemOptionRepository(MomPosContext context) {
            _context = context;
        }

        public async Task<IEnumerable<MenuItemOption>> GetAllAsync() {
            return await _context.MenuItemOptions.ToListAsync();
        }

        public async Task<MenuItemOption> GetByIdAsync(int id) {
            return await _context.MenuItemOptions.FirstOrDefaultAsync(mio => mio.MenuItemOptionId == id);
        }

        public async Task<MenuItemOption> AddAsync(MenuItemOption entity) {
            _context.MenuItemOptions.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MenuItemOption> UpdateAsync(MenuItemOption entity) {
            _context.MenuItemOptions.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id) {
            var menuItemOption = await _context.MenuItemOptions.FindAsync(id);
            if (menuItemOption == null) {
                return false;
            }

            _context.MenuItemOptions.Remove(menuItemOption);
            await _context.SaveChangesAsync();
            return true;
        }
    }

}