using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MomPosApi.Models
{
    public class Menu
    {
        [Key]public int menu_id { get; set; }
        public string? item_name { get; set; }
        public string? item_description { get; set; }
        public decimal price { get; set; }
        public string? category { get; set; }
        public bool available { get; set; }
    }
}