using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class Order {
        [Key]
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int TotalAmount { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = [];
    }

}
