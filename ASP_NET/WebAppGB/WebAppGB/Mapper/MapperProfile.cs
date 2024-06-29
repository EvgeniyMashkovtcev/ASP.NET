using AutoMapper;
using WebAppGB.Dto;
using WebAppGB.Models;

namespace WebAppGB.Mapper
{
    public class MapperProfile : Profile 
    {
        public MapperProfile() 
        { 
            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<ProductGroup, ProductGroupDto>().ReverseMap();
        }
    }
}
