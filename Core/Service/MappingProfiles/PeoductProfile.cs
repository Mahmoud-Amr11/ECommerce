using AutoMapper;
using DomainLayer.Models;
using Shared.DataTransferObject;

namespace Service.MappingProfiles
{
    public class ProductProfile :Profile
    {
        public ProductProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.TypeName, option => option.MapFrom(src => src.ProductType.Name))
                .ForMember(dest => dest.BrandName, option => option.MapFrom(src => src.ProductBrand.Name))
                 .ForMember(dest => dest.PictureUrl, option => option.MapFrom<PictureUrlResolver>());

            CreateMap<ProductBrand, BrandDto>();
            CreateMap<ProductType, TypeDto>();
        }
    }
}
