using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class MenuItem {
        [Key]
        public int MenuItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public required string Status { get; set; }
        public required string PhotoUrl { get; set; }
        public required ICollection<MenuItemOption> Options { get; set; }
        public required ICollection<CategoryMenuItem> CategoryMenuItems { get; set; }
    }
}
