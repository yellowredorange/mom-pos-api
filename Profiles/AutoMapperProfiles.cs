using AutoMapper;
using MomPosApi.Models;

namespace MomPosApi.Profiles {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<MenuConfiguration, MenuConfigurationDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            CreateMap<MenuItemOption, MenuItemOptionDto>().ReverseMap();
            CreateMap<Order, OrderDto>();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name))
            .ForMember(dest => dest.Options, opt => opt.MapFrom(src => SplitOptions(src.Options)));

        }
        private List<string> SplitOptions(string options) {
            return options?.Split(',').ToList() ?? [];
        }
    }
}