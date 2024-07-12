using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class OrderItem {
        [Key]
        public int OrderItemId { get; set; }
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int MenuItemId { get; set; }
        public MenuItem? MenuItem { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Options { get; set; } = string.Empty;
    }

}
