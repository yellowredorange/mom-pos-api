using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class CategoryMenuItem {
        [Key]
        public int CategoryMenuItemId { get; set; }
        public int CategoryId { get; set; }
        public int MenuItemId { get; set; }
        public int SortOrder { get; set; }

        public required Category Category { get; set; }
        public required MenuItem MenuItem { get; set; }
    }
}
