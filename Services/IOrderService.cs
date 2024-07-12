using MomPosApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
public interface IOrderService {
  Task<List<OrderResponseDto>> GetAllOrdersAsync();
  Task<OrderResponseDto> GetOrderByIdAsync(int id);
  Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request);
  Task<OrderResponseDto> UpdateOrderAsync(Order order);
  Task<bool> DeleteOrderAsync(int id);
}