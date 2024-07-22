using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class Order {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required ICollection<OrderItem> OrderItems { get; set; }
    }

    public class OrderItem {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal UnitPrice { get; set; }
        public string Options { get; set; } = string.Empty;
    }

    public class OrderDto {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required List<OrderItemDto> OrderItems { get; set; }
    }

    public class OrderOptionDto {
        public required string OptionCategory { get; set; }
        public required string OptionName { get; set; }
        public decimal AdditionalPrice { get; set; }
    }

    public class OrderItemDto {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public required List<OrderOptionDto> Options { get; set; }
    }

    public class CreateOrderRequestDto {
        public required List<OrderItemDto> Items { get; set; }
    }

    public class OrderItemResponseDto {
        public int OrderItemId { get; set; }
        public int MenuItemId { get; set; }
        public required string MenuItemName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public required List<string> Options { get; set; }
        public decimal TotalPrice { get; set; }
    }

    public class OrderResponseDto {
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public required List<OrderItemResponseDto> OrderItems { get; set; }
    }
}