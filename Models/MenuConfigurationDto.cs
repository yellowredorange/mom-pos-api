using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomPosApi.Models {
    public class MenuConfigurationDto {
        public int MenuConfigurationId { get; set; }
        public required string Name { get; set; }
        public bool IsActive { get; set; }
        public int SortOrder { get; set; }
        public List<int> CategoryIds { get; set; } = [];
    }
}