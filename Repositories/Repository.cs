using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly MomPosContext _context;
    private readonly ILogger<Repository<T>> _logger;

    public Repository(MomPosContext context, ILogger<Repository<T>> logger)
    {
        _context = context;
        _logger = logger;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        try
        {
            return await _context.Set<T>().ToListAsync();
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving all entities of type {EntityType}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<T> GetByIdAsync(int id)
    {
        try
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity == null)
            {
                _logger.LogWarning("Entity of type {EntityType} with id {Id} not found", typeof(T).Name, id);
                throw new KeyNotFoundException($"Entity of type {typeof(T).Name} with id {id} not found.");
            }
            return entity;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while retrieving the entity of type {EntityType} with id {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        try
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        } catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred while adding a new entity of type {EntityType}", typeof(T).Name);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding a new entity of type {EntityType}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        try
        {
            await _context.Set<T>().AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        } catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred while adding new entities of type {EntityType}", typeof(T).Name);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while adding new entities of type {EntityType}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        } catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "A concurrency conflict occurred while updating the entity of type {EntityType}", typeof(T).Name);
            throw;
        } catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred while updating the entity of type {EntityType}", typeof(T).Name);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the entity of type {EntityType}", typeof(T).Name);
            throw;
        }
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        try
        {
            var entity = await GetByIdAsync(id);
            if (entity == null)
            {
                return false;
            }

            _context.Set<T>().Remove(entity);
            await _context.SaveChangesAsync();
            return true;
        } catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred while deleting the entity of type {EntityType} with id {Id}", typeof(T).Name, id);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while deleting the entity of type {EntityType} with id {Id}", typeof(T).Name, id);
            throw;
        }
    }

    public virtual async Task UpdateBatchAsync(IEnumerable<T> entities)
    {
        try
        {
            foreach (var entity in entities)
            {
                _context.Entry(entity).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException ex)
        {
            _logger.LogError(ex, "A concurrency conflict occurred while updating entities of type {EntityType}", typeof(T).Name);
            throw;
        } catch (DbUpdateException ex)
        {
            _logger.LogError(ex, "A database update error occurred while updating entities of type {EntityType}", typeof(T).Name);
            throw;
        } catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating entities of type {EntityType}", typeof(T).Name);
            throw;
        }
    }
}
