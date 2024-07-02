using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models {
    public class MenuConfigurationCategory {
        [Key]
        public int MenuConfigurationCategoryId { get; set; }
        public int MenuConfigurationId { get; set; }
        public int CategoryId { get; set; }
        public int SortOrder { get; set; }

        public required MenuConfiguration MenuConfiguration { get; set; }
        public required Category Category { get; set; }
    }
}
