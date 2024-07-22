using AutoMapper;
using MomPosApi.Models;

public class OrderService : IOrderService {
  private readonly IOrderRepository _orderRepository;
  private readonly IRepository<MenuItem> _menuItemRepository;
  private readonly IMapper _mapper;

  public OrderService(IOrderRepository orderRepository, IRepository<MenuItem> menuItemRepository, IMapper mapper) {
    _orderRepository = orderRepository;
    _menuItemRepository = menuItemRepository;
    _mapper = mapper;
  }

  public async Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequestDto request) {
    var order = new Order {
      OrderDate = DateTime.UtcNow,
      OrderItems = new List<OrderItem>()
    };

    decimal totalAmount = 0;

    foreach (var itemDto in request.Items) {
      var menuItem = await _menuItemRepository.GetByIdAsync(itemDto.MenuItemId);
      if (menuItem == null) {
        throw new ArgumentException($"Menu item with id {itemDto.MenuItemId} not found");
      }

      var orderItem = new OrderItem {
        MenuItemId = itemDto.MenuItemId,
        MenuItem = menuItem,
        Quantity = itemDto.Quantity,
        UnitPrice = menuItem.Price,
        Options = string.Join(",", itemDto.Options.Select(o => $"{o.OptionCategory}:{o.OptionName}"))
      };

      // 計算選項的額外價格
      var optionsList = new List<string>();
      foreach (var optionDto in itemDto.Options) {
        var menuItemOption = menuItem.MenuItemOptions.FirstOrDefault(o =>
            o.OptionCategory == optionDto.OptionCategory && o.Option == optionDto.OptionName);

        if (menuItemOption != null) {
          orderItem.UnitPrice += menuItemOption.AdditionalPrice;
          optionsList.Add($"{optionDto.OptionCategory}:{optionDto.OptionName}");

        }
      }
      orderItem.Options = optionsList.Count > 0 ? string.Join(",", optionsList) : string.Empty;
      orderItem.TotalPrice = orderItem.UnitPrice * orderItem.Quantity;
      totalAmount += orderItem.TotalPrice;

      order.OrderItems.Add(orderItem);
    }

    order.TotalAmount = totalAmount;

    var createdOrder = await _orderRepository.AddAsync(order);

    return _mapper.Map<OrderResponseDto>(createdOrder);
  }

  public async Task<OrderResponseDto> GetOrderByIdAsync(int id) {
    var order = await _orderRepository.GetByIdAsync(id);
    if (order == null) {
      throw new($"Order with ID {id} not found");
    }
    return _mapper.Map<OrderResponseDto>(order);
  }

  public async Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync() {
    var orders = await _orderRepository.GetAllAsync();
    return _mapper.Map<IEnumerable<OrderResponseDto>>(orders);
  }

  public async Task<bool> DeleteOrderAsync(int id) {
    return await _orderRepository.DeleteAsync(id);
  }
}