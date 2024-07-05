using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

namespace MomPosApi.Repositories {
    public class MenuConfigurationRepository : IMenuConfigurationRepository {
        private readonly MomPosContext _context;

        public MenuConfigurationRepository(MomPosContext context) {
            _context = context;
        }

        public async Task<IEnumerable<MenuConfiguration>> GetAllAsync() {
            return await _context.MenuConfigurations.Include(mc => mc.Categories).ToListAsync();
        }

        public async Task<MenuConfiguration> GetByIdAsync(int id) {
            return await _context.MenuConfigurations.Include(mc => mc.Categories).FirstOrDefaultAsync(mc => mc.MenuConfigurationId == id);
        }

        public async Task<MenuConfiguration> AddAsync(MenuConfiguration entity) {
            _context.MenuConfigurations.Add(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<MenuConfiguration> UpdateAsync(MenuConfiguration entity) {
            _context.MenuConfigurations.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<bool> DeleteAsync(int id) {
            var menuConfiguration = await _context.MenuConfigurations.FindAsync(id);
            if (menuConfiguration == null) {
                return false;
            }

            _context.MenuConfigurations.Remove(menuConfiguration);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<IEnumerable<MenuConfiguration>> GetAllMenusAsync() {
            return await _context.MenuConfigurations
                .Include(mc => mc.Categories)
                .ThenInclude(c => c.MenuItems)
                .ThenInclude(mi => mi.MenuItemOptions)
                .ToListAsync();
        }

    }

}