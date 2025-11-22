using AutoMapper;
using DomainLayer.Models.IdentityModels;
using Shared.DataTransferObject.IdentityDto;

namespace Service.MappingProfiles
{
    public class IdentityProfile:Profile
    {
        public IdentityProfile()
        {
           CreateMap<Address, AddressDto>().ReverseMap();
        }
    }
}
