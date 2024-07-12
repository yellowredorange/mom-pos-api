using MomPosApi.Models;
namespace MomPosApi.Models {
  public class OrderResponseDto {
    public int OrderId { get; set; }
    public DateTime OrderDate { get; set; }
    public decimal TotalAmount { get; set; }
    public required List<OrderItemResponseDto> OrderItems { get; set; }
  }
}