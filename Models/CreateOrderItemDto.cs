using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class CreateOrderItemDto {
        public int MenuItemId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public List<string>? Options { get; set; }
    }
}
