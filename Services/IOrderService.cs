using MomPosApi.Models;
public interface IOrderService
{
    Task<OrderResponseDto> CreateOrderAsync(CreateOrderRequestDto request);
    Task<OrderResponseDto> GetOrderByIdAsync(int id);
    Task<IEnumerable<OrderResponseDto>> GetAllOrdersAsync();
    Task<bool> DeleteOrderAsync(int id);
}