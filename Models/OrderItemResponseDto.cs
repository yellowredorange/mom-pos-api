namespace MomPosApi.Models {
  public class OrderItemResponseDto {
    public int OrderItemId { get; set; }
    public int MenuItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public string? Options { get; set; }
    public required string MenuItemName { get; set; }
  }
}