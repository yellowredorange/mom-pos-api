using System.ComponentModel.DataAnnotations;

namespace MomPosApi.Models
{
    public class MenuItemOption
    {
        [Key]
        public int MenuItemOptionId { get; set; }
        public required string Option { get; set; }
        public required string OptionCategory { get; set; }
        public decimal AdditionalPrice { get; set; }
        public int SortOrder { get; set; }
        public int MenuItemId { get; set; }
        public required MenuItem MenuItem { get; set; }
    }

}
