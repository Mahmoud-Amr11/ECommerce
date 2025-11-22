using AutoMapper;
using DomainLayer.Models.OrderModules;
using Microsoft.Extensions.Configuration;
using Shared.DataTransferObject.OrderDtos;

namespace Service.MappingProfiles
{
    public class OrderItemPictureResolver(IConfiguration _configuration) : IValueResolver<OrderItem, OrderItemDto, string>
    {
        public string Resolve(OrderItem source, OrderItemDto destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.Product.PictureUrl))
            {
                return string.Empty;
            }
            return $"{_configuration.GetSection("Urls")["BaseUrl"]}{source.Product.PictureUrl}";
        }
    }
}
