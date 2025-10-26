using AutoMapper;
using DomainLayer.Models.BasketModule;
using Shared.DataTransferObject.BasketDtos;

namespace Service.MappingProfiles
{
    public class BasketProfile : Profile
    {
        public BasketProfile()
        {
            CreateMap<Basket, BasketDto>().ReverseMap();
            CreateMap<BasketItem, BasketItemDto>().ReverseMap();
        }
    }
}
