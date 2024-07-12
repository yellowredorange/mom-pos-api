using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MomPosApi.Models;

namespace MomPosApi.Data {
    public class DataSeeder {
        public static void SeedData(MomPosContext context) {
            if (context.MenuConfigurations.Any() || context.Categories.Any() || context.MenuItems.Any() || context.MenuItemOptions.Any()) {
                return;   // 數據庫已經有數據，不需要再次添加
            }

            // 添加 MenuConfiguration
            var menuConfig = new MenuConfiguration {
                Name = "主菜單",
                IsActive = true,
                SortOrder = 1
            };
            context.MenuConfigurations.Add(menuConfig);
            context.SaveChanges();

            // 添加 Category
            var categories = new List<Category>
            {
    new Category { Name = "主菜", MenuConfigurationId = menuConfig.MenuConfigurationId, MenuConfiguration = menuConfig, IsActive = true, SortOrder = 1 },
        new Category { Name = "飲料", MenuConfigurationId = menuConfig.MenuConfigurationId, MenuConfiguration = menuConfig, IsActive = true, SortOrder = 2 },
        new Category { Name = "甜點", MenuConfigurationId = menuConfig.MenuConfigurationId, MenuConfiguration = menuConfig, IsActive = true, SortOrder = 3 }
    };
            context.Categories.AddRange(categories);
            context.SaveChanges();

            // 添加 MenuItem
            var menuItems = new List<MenuItem>
            {
       new MenuItem {
            Name = "牛肉麵",
            Description = "傳統台灣牛肉麵",
            Price = 120m,
            IsActive = true,
            PhotoUrl = "beef_noodle.jpg",
            CategoryId = categories[0].CategoryId,
            Category = categories[0],  // 設置 Category
            SortOrder = 1
        },
        new MenuItem {
            Name = "珍珠奶茶",
            Description = "經典珍珠奶茶",
            Price = 60m,
            IsActive = true,
            PhotoUrl = "bubble_tea.jpg",
            CategoryId = categories[1].CategoryId,
            Category = categories[1],  // 設置 Category
            SortOrder = 1
        },
        new MenuItem {
            Name = "芒果冰",
            Description = "新鮮芒果冰",
            Price = 80m,
            IsActive = true,
            PhotoUrl = "mango_ice.jpg",
            CategoryId = categories[2].CategoryId,
            Category = categories[2],  // 設置 Category
            SortOrder = 1
        }
    };
            context.MenuItems.AddRange(menuItems);
            context.SaveChanges();

            // 添加 MenuItemOption
            var menuItemOptions = new List<MenuItemOption>
            {
        new MenuItemOption {
            Option = "加麵",
            OptionCategory = "加料",
            AdditionalPrice = 10m,
            SortOrder = 1,
            MenuItemId = menuItems[0].MenuItemId,
            MenuItem = menuItems[0]  // 設置 MenuItem
        },
        new MenuItemOption {
            Option = "去冰",
            OptionCategory = "冰量",
            AdditionalPrice = 0m,
            SortOrder = 1,
            MenuItemId = menuItems[1].MenuItemId,
            MenuItem = menuItems[1]  // 設置 MenuItem
        },
        new MenuItemOption {
            Option = "加芒果",
            OptionCategory = "加料",
            AdditionalPrice = 20m,
            SortOrder = 1,
            MenuItemId = menuItems[2].MenuItemId,
            MenuItem = menuItems[2]  // 設置 MenuItem
        }
    };
            context.MenuItemOptions.AddRange(menuItemOptions);
            context.SaveChanges();
        }
    }
}