using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomPosApi.Models {
    public class MenuItemDto {
        public int MenuItemId { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; }
        public required string PhotoUrl { get; set; }
        public int SortOrder { get; set; }
    }
}