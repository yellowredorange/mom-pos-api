public interface IRepository<T> where T : class
{
    Task<T> AddAsync(T entity);
    Task AddRangeAsync(IEnumerable<T> entities);

    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);

    Task<T> UpdateAsync(T entity);
    Task UpdateBatchAsync(IEnumerable<T> entities);

    Task<bool> DeleteAsync(int id);
}