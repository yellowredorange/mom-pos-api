using Microsoft.EntityFrameworkCore;
using MomPosApi.Data;
using MomPosApi.Models;
using System.Collections.Generic;
using System.Threading.Tasks;


public class OrderRepository : IOrderRepository {
  private readonly MomPosContext _context;

  public OrderRepository(MomPosContext context) {
    _context = context;
  }

  public async Task<List<Order>> GetAllAsync() {
    return await _context.Orders.Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.MenuItem)
                                .ToListAsync();
  }

  public async Task<Order> GetByIdAsync(int id) {
    return await _context.Orders.Include(o => o.OrderItems)
                                .ThenInclude(oi => oi.MenuItem)
                                .FirstOrDefaultAsync(o => o.OrderId == id);
  }

  public async Task<Order> AddAsync(Order entity) {
    _context.Orders.Add(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<Order> UpdateAsync(Order entity) {
    _context.Orders.Update(entity);
    await _context.SaveChangesAsync();
    return entity;
  }

  public async Task<bool> DeleteAsync(int id) {
    var order = await _context.Orders.FindAsync(id);
    if (order == null) {
      return false;
    }

    _context.Orders.Remove(order);
    await _context.SaveChangesAsync();
    return true;
  }
}
