using AutoMapper;
using MomPosApi.Models;

namespace MomPosApi.Profiles {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Category, CategoryDto>()
                .ForMember(dest => dest.MenuItemIds, opt => opt.MapFrom(src => src.MenuItems.Select(mi => mi.MenuItemId)))
                .ReverseMap();

            CreateMap<MenuItem, MenuItemDto>().ReverseMap();

            CreateMap<MenuConfiguration, MenuConfigurationDto>()
                .ForMember(dest => dest.CategoryIds, opt => opt.MapFrom(src => src.Categories.Select(c => c.CategoryId)))
                .ReverseMap();

            CreateMap<MenuItemOption, MenuItemOptionDto>().ReverseMap();
        }
    }
}