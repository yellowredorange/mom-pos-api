using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models
{
    public class MenuConfiguration
    {
        [Key]
        public int MenuConfigurationId { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public ICollection<Category> Categories { get; set; } = [];
    }
}
