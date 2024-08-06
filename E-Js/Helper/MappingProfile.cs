using ApplicationCore.Enities;
using AutoMapper;
using E_Js.Dtos;

namespace E_Js.Helper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>();
            CreateMap<Brand, BrandDto>()
                .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Title));
            CreateMap<Category, CategoryDto>()
                .ForMember(dto => dto.Name, options => options.MapFrom(src => src.Title));
        }
    }
}
