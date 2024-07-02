using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class Category {
        [Key]
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public int SortOrder { get; set; }
        public required ICollection<CategoryMenuItem> CategoryMenuItems { get; set; }
        public required ICollection<MenuConfigurationCategory> MenuConfigurationCategories { get; set; }
    }
}
