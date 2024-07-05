using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;

public class CategoryRepository : ICategoryRepository {
    private readonly MomPosContext _context;

    public CategoryRepository(MomPosContext context) {
        _context = context;
    }

    public async Task<IEnumerable<Category>> GetAllAsync() {
        return await _context.Categories.Include(c => c.MenuItems).ToListAsync();
    }

    public async Task<Category> GetByIdAsync(int id) {
        return await _context.Categories.Include(c => c.MenuItems).FirstOrDefaultAsync(c => c.CategoryId == id);
    }

    public async Task<Category> AddAsync(Category entity) {
        _context.Categories.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<Category> UpdateAsync(Category entity) {
        _context.Categories.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id) {
        var category = await _context.Categories.FindAsync(id);
        if (category == null) {
            return false;
        }

        _context.Categories.Remove(category);
        await _context.SaveChangesAsync();
        return true;
    }
}
