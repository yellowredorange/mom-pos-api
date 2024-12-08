using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models
{
    public class Category
    {
        [Key]
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public int MenuConfigurationId { get; set; }
        public bool IsActive { get; set; }
        public required MenuConfiguration MenuConfiguration { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = [];
        public int SortOrder { get; set; }
    }


}
