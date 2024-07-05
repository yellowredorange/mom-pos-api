using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class MenuItem {
        [Key]
        public int MenuItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public required string PhotoUrl { get; set; }
        public int CategoryId { get; set; }
        public ICollection<MenuItemOption> MenuItemOptions { get; set; } = [];
        public required Category Category { get; set; }
        public MenuItemOption? MenuItemOption { get; set; }
        public int SortOrder { get; set; }
    }
}