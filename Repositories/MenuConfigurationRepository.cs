using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

namespace MomPosApi.Repositories {
    public class MenuConfigurationRepository : Repository<MenuConfiguration>, IMenuConfigurationRepository {
        public MenuConfigurationRepository(MomPosContext context) : base(context) {
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