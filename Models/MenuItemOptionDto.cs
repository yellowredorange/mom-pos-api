using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MomPosApi.Models {
    public class MenuItemOptionDto {
        public int MenuItemOptionId { get; set; }
        public required string Option { get; set; }
        public required string OptionCategory { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int SortOrder { get; set; }
    }
}