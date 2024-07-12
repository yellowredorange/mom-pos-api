using AutoMapper;
using MomPosApi.Models;

namespace MomPosApi.Profiles {
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<MenuConfiguration, MenuConfigurationDto>().ReverseMap();
            CreateMap<MenuItem, MenuItemDto>().ReverseMap();
            CreateMap<MenuItemOption, MenuItemOptionDto>().ReverseMap(); ;
        }
    }
}