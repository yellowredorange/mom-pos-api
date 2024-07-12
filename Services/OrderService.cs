using MomPosApi.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
public class OrderService : IOrderService {
  private readonly IOrderRepository _orderRepository;
  private readonly IMenuItemRepository _menuItemRepository;
  private readonly IMapper _mapper;
  private readonly ILogger<OrderService> _logger;

  public OrderService(
      IOrderRepository orderRepository,
      IMenuItemRepository menuItemRepository,
      IMapper mapper,
      ILogger<OrderService> logger) {
    _orderRepository = orderRepository;
    _menuItemRepository = menuItemRepository;
    _mapper = mapper;
    _logger = logger;
  }
  public async Task<List<OrderResponseDto>> GetAllOrdersAsync() {
    var orders = await _orderRepository.GetAllAsync();
    return _mapper.Map<List<OrderResponseDto>>(orders);
  }

  public async Task<OrderResponseDto> GetOrderByIdAsync(int id) {
    var order = await _orderRepository.GetByIdAsync(id);
    return _mapper.Map<OrderResponseDto>(order);
  }

  public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequest request) {
    try {
      var order = new Order {
        OrderDate = DateTime.UtcNow,
        OrderItems = new List<OrderItem>()
      };

      foreach (var item in request.Items) {
        var menuItem = await _menuItemRepository.GetByIdAsync(item.MenuItemId);
        if (menuItem == null) {
          throw new ArgumentException($"Menu item with id {item.MenuItemId} not found");
        }

        var orderItem = _mapper.Map<OrderItem>(item);
        orderItem.Options = string.Join(",", item.Options);
        order.OrderItems.Add(orderItem);
      }

      order.TotalAmount = (int)order.OrderItems.Sum(oi => oi.Quantity * oi.UnitPrice);

      var createdOrder = await _orderRepository.AddAsync(order);
      return _mapper.Map<OrderResponseDto>(createdOrder);
    } catch (Exception ex) {
      _logger.LogError(ex, "Error creating order");
      throw;
    }
  }

  public async Task<OrderResponseDto> UpdateOrderAsync(Order order) {
    var updatedOrder = await _orderRepository.UpdateAsync(order);
    return _mapper.Map<OrderResponseDto>(updatedOrder);
  }

  public async Task<bool> DeleteOrderAsync(int id) {
    return await _orderRepository.DeleteAsync(id);
  }
}
