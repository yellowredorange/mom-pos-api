using AutoMapper;
using MomPosApi.Models;

namespace MomPosApi.Profiles {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<MenuConfiguration, MenuConfigurationDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            CreateMap<MenuItemOption, MenuItemOptionDto>().ReverseMap();
            CreateMap<Order, OrderResponseDto>();
            CreateMap<OrderItem, OrderItemResponseDto>()
                .ForMember(dest => dest.MenuItemName, opt => opt.MapFrom(src => src.MenuItem.Name));
            CreateMap<CreateOrderItemDto, OrderItem>();
        }
    }
}