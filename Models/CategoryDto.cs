using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomPosApi.Models {
    public class CategoryDto {
        public int CategoryId { get; set; }
        public required string Name { get; set; }
        public int MenuConfigurationId { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
    }
}