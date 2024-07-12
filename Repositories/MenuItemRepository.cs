using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;
public class MenuItemRepository : IMenuItemRepository {
    private readonly MomPosContext _context;

    public MenuItemRepository(MomPosContext context) {
        _context = context;
    }

    public async Task<IEnumerable<MenuItem>> GetAllAsync() {
        return await _context.MenuItems.Include(mi => mi.MenuItemOptions).ToListAsync();
    }

    public async Task<MenuItem> GetByIdAsync(int id) {
        return await _context.MenuItems.Include(mi => mi.MenuItemOptions).FirstOrDefaultAsync(mi => mi.MenuItemId == id);
    }

    public async Task<MenuItem> AddAsync(MenuItem entity) {
        _context.MenuItems.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<MenuItem> UpdateAsync(MenuItem entity) {
        _context.MenuItems.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id) {
        var menuItem = await _context.MenuItems.FindAsync(id);
        if (menuItem == null) {
            return false;
        }

        _context.MenuItems.Remove(menuItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
