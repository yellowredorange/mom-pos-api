using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;

public class Repository<T> : IRepository<T> where T : class {
    protected readonly MomPosContext _context;

    public Repository(MomPosContext context) {
        _context = context;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() {
        return await _context.Set<T>().ToListAsync();
    }

    public virtual async Task<T> GetByIdAsync(int id) {
        var entity = await _context.Set<T>().FindAsync(id);
        if (entity == null) {
            throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
        }
        return entity;
    }

    public virtual async Task<T> AddAsync(T entity) {
        await _context.Set<T>().AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities) {
        await _context.Set<T>().AddRangeAsync(entities);
        await _context.SaveChangesAsync();
    }

    public virtual async Task<T> UpdateAsync(T entity) {
        _context.Set<T>().Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(int id) {
        var entity = await GetByIdAsync(id);
        if (entity == null) {
            return false;
        }

        _context.Set<T>().Remove(entity);
        await _context.SaveChangesAsync();
        return true;
    }
}