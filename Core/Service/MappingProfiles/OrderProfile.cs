using AutoMapper;
using DomainLayer.Models.OrderModules;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;

namespace Service.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>().ReverseMap();

            CreateMap<Order, OrderToReturnDto>()
                .ForMember(dist=>dist.DeliveryMethod, opt=>opt.MapFrom(src=>src.DeliveryMethod.ShortName))             ;

            CreateMap<OrderItem, OrderItemDto>()
               .ForMember(dist => dist.ProductName, options => options.MapFrom(src => src.Product.ProductName))
               .ForMember(dist => dist.PictureUrl, options => options.MapFrom<OrderItemPictureResolver>());

            CreateMap<DeliveryMethod, DeliveryMethodDto>();

        }
    }
}
