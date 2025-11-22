using AutoMapper;
using DomainLayer.Models.OrderModules;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.MappingProfiles
{
    public class OrderProfile:Profile
    {
        public OrderProfile()
        {
            CreateMap<AddressDto, OrderAddress>();
            CreateMap<Order, OrderToReturnDto>();
               

        }
    }
}
