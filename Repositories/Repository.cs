using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;

public class Repository<T> : IRepository<T> where T : class {
    private readonly MomPosContext _context;
    private readonly DbSet<T> _dbSet;

    public Repository(MomPosContext context) {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<IEnumerable<T>> GetAllAsync() {
        return await _dbSet.ToListAsync();
    }

    public async Task<T> GetByIdAsync(int id) {
        return await _dbSet.FindAsync(id);
    }

    public async Task<T> AddAsync(T entity) {
        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<T> UpdateAsync(T entity) {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<bool> DeleteAsync(int id) {
        var entity = await _dbSet.FindAsync(id);
        if (entity == null) {
            return false;
        }

        _dbSet.Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}
