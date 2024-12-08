using MomPosApi.Models;

public class MenuConfigurationUpdateDto
{
    public List<CategoryDto>? UpdatedCategories { get; set; }
    public List<MenuItemDto>? UpdatedMenuItems { get; set; }
    public List<MenuItemOptionDto>? UpdatedMenuItemOptions { get; set; }
}