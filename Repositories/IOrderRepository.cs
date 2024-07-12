using MomPosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IOrderRepository {
  Task<List<Order>> GetAllAsync();
  Task<Order> GetByIdAsync(int id);
  Task<Order> AddAsync(Order entity);
  Task<Order> UpdateAsync(Order entity);
  Task<bool> DeleteAsync(int id);
}
