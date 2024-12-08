using MomPosApi.Models;
using MomPosApi.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

public class DataSeeder
{
    private readonly IMenuConfigurationService _menuConfigurationService;
    private readonly ICategoryService _categoryService;
    private readonly IMenuItemService _menuItemService;
    private readonly IMenuItemOptionService _menuItemOptionService;

    public DataSeeder(
        IMenuConfigurationService menuConfigurationService,
        ICategoryService categoryService,
        IMenuItemService menuItemService,
        IMenuItemOptionService menuItemOptionService)
    {
        _menuConfigurationService = menuConfigurationService;
        _categoryService = categoryService;
        _menuItemService = menuItemService;
        _menuItemOptionService = menuItemOptionService;
    }

    public async Task SeedDataAsync()
    {
        var menuConfiguration = new MenuConfigurationDto
        {
            Name = "AoDai",
            IsActive = true,
            SortOrder = 1
        };

        var categories = new List<CategoryDto>
        {
            new CategoryDto { Name = "河粉", MenuConfigurationId = 1, IsActive = true, SortOrder = 1 },
            new CategoryDto { Name = "米線", MenuConfigurationId = 1, IsActive = true, SortOrder = 2 },
            new CategoryDto { Name = "泡麵", MenuConfigurationId = 1, IsActive = true, SortOrder = 3 },
            new CategoryDto { Name = "飯類", MenuConfigurationId = 1, IsActive = true, SortOrder = 4 },
            new CategoryDto { Name = "湯品", MenuConfigurationId = 1, IsActive = true, SortOrder = 5 },
            new CategoryDto { Name = "越式小吃", MenuConfigurationId = 1, IsActive = true, SortOrder = 6 },
            new CategoryDto { Name = "麵包手捲", MenuConfigurationId = 1, IsActive = true, SortOrder = 7 },
            new CategoryDto { Name = "甜品飲料", MenuConfigurationId = 1, IsActive = true, SortOrder = 8 }
        };

        var menuItems = new List<MenuItemDto>
        {
            new MenuItemDto { Name = "鮮牛肉肉丸河粉", Description = "精選新鮮牛肉搭配手工肉丸，口感鮮嫩多汁，每一口都充滿豐富的牛肉香氣。內含牛肉。", Price = 140, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "鮮牛肉河粉", Description = "經典的鮮牛肉河粉，湯頭清爽，搭配鮮嫩的牛肉片，讓您品嚐到最純正的越南風味。內含牛肉。", Price = 120, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "總統河粉", Description = "頂級總統河粉，選用最優質的牛肉，搭配特製湯頭，口感絕佳，適合品味高尚的您。內含牛肉。", Price = 250, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "越式紅燒牛肉河粉", Description = "融合越南獨特香料，湯頭濃郁，牛肉鮮嫩，讓您每一口都充滿異國風情。內含牛肉。", Price = 160, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "什錦炒河粉", Description = "多種食材精心烹調的什錦炒河粉，口感豐富，色香味俱全，帶給您滿滿的驚喜。內含豬肉和海鮮。", Price = 120, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "烤排骨乾河粉", Description = "香氣四溢的烤排骨乾河粉，排骨肉質鮮美，搭配河粉，讓您一口接一口，停不下來。內含豬肉。", Price = 110, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "炸春捲乾河粉", Description = "金黃酥脆的炸春捲乾河粉，外酥內嫩，搭配清爽的河粉，絕對是您不容錯過的美味。內含豬肉。", Price = 110, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 7 },
            new MenuItemDto { Name = "蝦醬乾河粉", Description = "獨特風味的蝦醬乾河粉，濃郁的蝦醬香氣撲鼻而來，讓人垂涎欲滴。內含蝦。", Price = 100, IsActive = true, CategoryId = 1, PhotoUrl = "", SortOrder = 8 },
            new MenuItemDto { Name = "原味海鮮米線", Description = "清淡爽口的原味海鮮米線，海鮮的鮮味完美融入湯頭，帶給您無與倫比的享受。內含海鮮。", Price = 120, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "酸辣海鮮米線", Description = "酸辣開胃的海鮮米線，濃郁的湯頭搭配鮮嫩的海鮮，讓您每一口都充滿驚喜。內含海鮮。", Price = 120, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "鮮牛肉肉丸米線", Description = "鮮牛肉搭配手工肉丸，湯頭濃郁，米線滑順，讓您享受美味的同時也能品味健康。內含牛肉。", Price = 140, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "鮮牛肉米線", Description = "經典的鮮牛肉米線，湯頭鮮美，牛肉嫩滑，每一口都讓您回味無窮。內含牛肉。", Price = 120, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "順化牛肉米線 (辣)", Description = "順化風味的辣牛肉米線，湯頭香辣濃郁，牛肉鮮嫩，讓您大快朵頤。內含牛肉。", Price = 140, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "越式紅燒牛肉米線", Description = "越式紅燒湯頭醇厚，牛肉軟嫩，風味獨特。內含牛肉。", Price = 160, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "烤排骨乾米線", Description = "香烤排骨搭配乾米線，口感豐富，味道絕佳。內含豬肉。", Price = 110, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 7 },
            new MenuItemDto { Name = "炸春捲乾米線", Description = "酥脆的炸春捲乾米線，外酥內嫩，口感一流。內含豬肉。", Price = 110, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 8 },
            new MenuItemDto { Name = "蝦醬乾米線", Description = "獨特風味的蝦醬乾米線，濃郁的蝦醬香氣撲鼻而來，讓人垂涎欲滴。內含蝦。", Price = 100, IsActive = true, CategoryId = 2, PhotoUrl = "", SortOrder = 9 },
            new MenuItemDto { Name = "原味海鮮泡麵", Description = "海鮮的鮮美與泡麵的滑順完美結合，帶給您豐富的口感享受。內含海鮮。", Price = 120, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "酸辣海鮮泡麵", Description = "酸辣開胃的海鮮泡麵，湯頭濃郁，海鮮鮮嫩，讓您每一口都充滿驚喜。內含海鮮。", Price = 120, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "鮮牛肉肉丸泡麵", Description = "鮮牛肉搭配肉丸的泡麵，湯頭濃郁，泡麵滑順，讓您享受美味的同時也能品味健康。內含牛肉。", Price = 140, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "鮮牛肉泡麵", Description = "經典的鮮牛肉泡麵，湯頭鮮美，牛肉嫩滑，每一口都讓您回味無窮。內含牛肉。", Price = 120, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "越式紅燒牛肉泡麵", Description = "越式紅燒湯頭醇厚，牛肉軟嫩，風味獨特。內含牛肉。", Price = 160, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "什錦炒泡麵", Description = "多種食材精心烹調的什錦炒泡麵，口感豐富，色香味俱全，帶給您滿滿的驚喜。內含豬肉和海鮮。", Price = 120, IsActive = true, CategoryId = 3, PhotoUrl = "", SortOrder = 6 },

            // 飯類
            new MenuItemDto { Name = "香茅烤排骨飯", Description = "排骨肉質鮮美，搭配香噴噴的米飯，讓您一口接一口，停不下來。內含豬肉。", Price = 120, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "魚露炸雞翅飯", Description = "雞翅酥脆可口，搭配特製魚露，風味獨特，讓您回味無窮。內含雞肉。", Price = 100, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "豬肉香茅蝦醬飯", Description = "香茅與蝦醬完美結合，味道鮮美，豬肉嫩滑，讓人食指大動。內含豬肉、蝦。", Price = 100, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "鮫魚蒸蛋飯", Description = "重口味鮫魚搭上蒸蛋調味，搭配香噴噴的米飯，讓您一口接一口，停不下來。內含魚肉。", Price = 100, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "香茅烤三層肉飯", Description = "三層肉質地豐富，搭配香茅的獨特香氣，讓人食指大動。內含豬肉。", Price = 130, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "雞腿飯", Description = "香嫩多汁的雞腿飯，搭配特製醬汁和香噴噴的米飯，是一道不可錯過的美味。內含雞肉。", Price = 130, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "蛋炒飯", Description = "經典的蛋炒飯，雞蛋的香氣融入每一粒米飯，簡單卻令人回味無窮。內含雞蛋。", Price = 90, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 7 },
            new MenuItemDto { Name = "牛肉炒飯", Description = "嫩滑的牛肉搭配香噴噴的炒飯，每一口都充滿了濃郁的肉香和米飯的香氣。內含牛肉。", Price = 120, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 8 },
            new MenuItemDto { Name = "蝦醬海鮮炒飯", Description = "辣度自選的蝦醬海鮮炒飯，蝦醬的香氣與海鮮的鮮味完美結合，帶給您豐富的口感體驗。內含海鮮。", Price = 130, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 9 },
            new MenuItemDto { Name = "鳳梨海鮮炒飯", Description = "甜美的鳳梨與鮮美的海鮮炒飯，帶來獨特的酸甜口感，讓您食指大動。內含海鮮。", Price = 130, IsActive = true, CategoryId = 4, PhotoUrl = "", SortOrder = 10 },

            // 湯品
            new MenuItemDto { Name = "越式酸辣湯", Description = "湯頭濃郁，酸辣適中，搭配鮮美的魚、蛤蜊或蝦子，讓您大快朵頤。內含海鮮。", Price = 130, IsActive = true, CategoryId = 5, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "海鮮酸辣湯", Description = "湯頭濃郁，酸辣適中，搭配豐富的海鮮，讓您每一口都充滿驚喜。內含海鮮。", Price = 220, IsActive = true, CategoryId = 5, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "香茅蛤蜊湯", Description = "香茅的獨特香氣與蛤蜊的鮮美完美結合，帶來清爽的口感。內含海鮮。", Price = 120, IsActive = true, CategoryId = 5, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "苦瓜包肉湯", Description = "湯頭清爽，苦瓜與肉的搭配讓人回味無窮。內含豬肉。", Price = 130, IsActive = true, CategoryId = 5, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "鮮牛肉肉丸湯", Description = "湯頭濃郁，牛肉鮮嫩，肉丸彈牙，絕對是您不容錯過的美味。內含牛肉。", Price = 100, IsActive = true, CategoryId = 5, PhotoUrl = "", SortOrder = 5 },

            // 越式小吃
            new MenuItemDto { Name = "蝦醬高麗菜", Description = "濃郁的蝦醬香氣與清脆的高麗菜完美融合，口感豐富。內含蝦醬。", Price = 100, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "燙青菜", Description = "簡單的燙青菜，保留了青菜的清爽口感和自然甜味，讓您品味健康。", Price = 30, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "月亮蝦餅 (滿滿蝦)", Description ="升級版月亮蝦餅，更多蝦的口感，帶給您大滿足。內含蝦。", Price = 250, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "月亮蝦餅", Description = "經典的月亮蝦餅，酥脆可口，每一口都讓您回味無窮。內含蝦。", Price = 200, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "蝦仁小煎餅", Description = "小煎餅外酥內嫩，蝦仁鮮美，絕對是您不容錯過的美味。內含蝦。", Price = 160, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "大蝦餅", Description = "大蝦餅酥脆可口，包裹著鮮蝦，讓您一口接一口，停不下來。內含蝦。", Price = 180, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "酥炸春捲", Description = "外酥內嫩，口感一流，本店招牌小菜，點了就對了。內含豬肉。", Price = 80, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 7 },
            new MenuItemDto { Name = "九層塔煎蛋", Description = "香氣四溢，雞蛋與九層塔的完美結合，讓人食指大動。內含雞蛋。", Price = 90, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 8 },
            new MenuItemDto { Name = "炸雞腿 (單點)", Description = "香脆多汁的炸雞腿，外酥內嫩，絕對是您不容錯過的美味。內含雞肉。", Price = 90, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 9 },
            new MenuItemDto { Name = "魚露炸雞翅", Description = "炸雞翅外酥內嫩，搭配特製魚露，風味獨特，讓您回味無窮。內含雞肉。", Price = 90, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 10 },
            new MenuItemDto { Name = "香茅烤排骨 (單點)", Description = "排骨肉質鮮美，搭配香茅的獨特香氣，讓人垂涎欲滴。內含豬肉。", Price = 70, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 11 },
            new MenuItemDto { Name = "烤三層肉 (單點)", Description = "香烤三層肉，外脆內嫩，肉質豐富，絕對是您不容錯過的美味。內含豬肉。", Price = 80, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 12 },
            new MenuItemDto { Name = "涼拌木瓜", Description = "清爽的涼拌木瓜，搭配特製調料，酸甜可口，讓人食指大動。內含木瓜。", Price = 80, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 13 },
            new MenuItemDto { Name = "涼拌海鮮青木瓜絲", Description = "海鮮的鮮味與青木瓜的爽脆完美結合，帶來獨特的酸甜口感。內含海鮮。", Price = 160, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 14 },
            new MenuItemDto { Name = "椰子蒸蝦", Description = "椰子的清香與蝦的鮮美完美融合，帶來獨特的風味享受。內含蝦。", Price = 200, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 15 },
            new MenuItemDto { Name = "黑胡椒蝦", Description = "蝦肉鮮美，黑胡椒的香氣撲鼻而來，讓人食指大動。內含蝦。", Price = 200, IsActive = true, CategoryId = 6, PhotoUrl = "", SortOrder = 16 },

            // 麵包手捲
            new MenuItemDto { Name = "火腿法國麵包", Description = "香脆的法國麵包搭配火腿，口感豐富，帶來滿滿的幸福感。內含豬肉。", Price = 100, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "越式法國麵包", Description = "經典的越式法國麵包，外脆內軟，搭配多種食材，口感豐富，讓人回味無窮。內含豬肉。", Price = 120, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "煉奶花生麵包", Description = "香甜的煉奶搭配花生，塗抹在香脆的麵包上，帶來滿滿的甜蜜滋味。", Price = 70, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "涼皮蝦捲 (3卷)", Description = "涼皮包裹鮮美蝦仁，清爽可口，每一口都充滿了蝦的鮮甜。內含蝦、豬肉。", Price = 160, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "涼皮春捲 (2卷)", Description = "清爽的涼皮春捲，搭配多種新鮮食材，帶來豐富的口感體驗。內含蝦、豬肉。", Price = 60, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "涼皮春捲 (3卷)", Description = "清爽的涼皮春捲，搭配多種新鮮食材，帶來豐富的口感體驗。內含蝦、豬肉。", Price = 80, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "綜合全餐手卷 (DIY)(2-3人份)", Description = "綜合全餐手卷，您可以自己動手搭配多種新鮮食材，享受DIY的樂趣。內含豬肉和海鮮。", Price = 300, IsActive = true, CategoryId = 7, PhotoUrl = "", SortOrder = 7 },

            // 甜品飲料
            new MenuItemDto { Name = "滴滴冰煉乳咖啡", Description = "甜度可選，咖啡的濃香與煉乳的甜蜜完美融合。內含煉乳。", Price = 60, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 1 },
            new MenuItemDto { Name = "越式冰拿鐵", Description = "甜度可選，口感濃郁，讓您回味無窮。內含鮮奶、煉乳。", Price = 90, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 2 },
            new MenuItemDto { Name = "椰奶甜湯", Description = "椰奶的濃香與甜味完美結合，讓您感受到熱帶風情。內含椰奶。", Price = 50, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 3 },
            new MenuItemDto { Name = "印尼奶茶", Description = "濃郁香甜，可選溫度，讓您品味不同風味的奶茶。",             Price = 50, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 4 },
            new MenuItemDto { Name = "法式越南優格", Description = "法式越南優格，口感濃郁，甜美可口，讓您享受健康美味。內含優格。", Price = 50, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 5 },
            new MenuItemDto { Name = "檸檬汽水", Description = "清爽的檸檬汽水，酸甜可口，讓您清涼一夏。", Price = 50, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 6 },
            new MenuItemDto { Name = "羅望子汁", Description = "酸甜適中，清爽可口，帶來獨特的風味享受。", Price = 50, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 7 },
            new MenuItemDto { Name = "可樂", Description = "經典的可樂，碳酸清爽，帶來滿滿的活力。", Price = 30, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 8 },
            new MenuItemDto { Name = "椰子水", Description = "爽解渴，帶來自然的椰子香氣，讓您感受熱帶風情。", Price = 30, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 9 },
            new MenuItemDto { Name = "越南檸檬風味綠茶 (罐裝)", Description = "清爽的越南檸檬風味綠茶，酸甜適中，讓您每一口都感受到清涼。", Price = 30, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 10 },
            new MenuItemDto { Name = "越南草莓口味提神汽水 (罐裝)", Description = "越南草莓口味提神汽水，甜美的草莓風味與清爽的汽水結合，帶來滿滿的活力。", Price = 30, IsActive = true, CategoryId = 8, PhotoUrl = "", SortOrder = 11 },
        };

        var menuItemOptions = new List<MenuItemOptionDto>
        {
            // 烤排骨乾河粉
            new MenuItemOptionDto { Option = "魚露", OptionCategory = "口味", AdditionalPrice = 0, SortOrder = 1, MenuItemId = 6 },
            new MenuItemOptionDto { Option = "醬油", OptionCategory = "口味", AdditionalPrice = 0, SortOrder = 2, MenuItemId = 6 },
            // 越式酸辣湯
            new MenuItemOptionDto { Option = "魚", OptionCategory = "種類", AdditionalPrice = 0, SortOrder = 1, MenuItemId = 34 },
            new MenuItemOptionDto { Option = "蛤蜊", OptionCategory = "種類", AdditionalPrice = 0, SortOrder = 2, MenuItemId = 34 },
            new MenuItemOptionDto { Option = "蝦子", OptionCategory = "種類", AdditionalPrice = 0, SortOrder = 3, MenuItemId = 34 },
            // 香茅蛤蜊湯
            new MenuItemOptionDto { Option = "清蒸", OptionCategory = "種類", AdditionalPrice = 0, SortOrder = 1, MenuItemId = 36 },
            new MenuItemOptionDto { Option = "湯", OptionCategory = "種類", AdditionalPrice = 0, SortOrder = 2, MenuItemId = 36 },
            // 滴滴冰煉乳咖啡
            new MenuItemOptionDto { Option = "煉乳3分", OptionCategory = "甜度", AdditionalPrice = 0, SortOrder = 1, MenuItemId = 62 },
            new MenuItemOptionDto { Option = "煉乳6分", OptionCategory = "甜度", AdditionalPrice = 0, SortOrder = 2, MenuItemId = 62 },
            new MenuItemOptionDto { Option = "煉乳9分", OptionCategory = "甜度", AdditionalPrice = 0, SortOrder = 3, MenuItemId = 62 },
            // 印尼奶茶
            new MenuItemOptionDto { Option = "冰", OptionCategory = "溫度", AdditionalPrice = 0, SortOrder = 1, MenuItemId = 65 },
            new MenuItemOptionDto { Option = "溫", OptionCategory = "溫度", AdditionalPrice = 0, SortOrder = 2, MenuItemId = 65 },
            new MenuItemOptionDto { Option = "熱", OptionCategory = "溫度", AdditionalPrice = 0, SortOrder = 3, MenuItemId = 65 },
        };

        // 批量插入数据
        await _menuConfigurationService.AddAsync(menuConfiguration);
        await _categoryService.AddRangeAsync(categories);
        await _menuItemService.AddRangeAsync(menuItems);
        await _menuItemOptionService.AddRangeAsync(menuItemOptions);
    }
}



