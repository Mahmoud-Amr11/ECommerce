using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using MediatR;
using Shared.DataTransferObject.BasketDtos;

namespace Service.BasketFeatures.Commands.AddOrUpdateBasket
{
    public class AddOrUpdateCommandHandler(IBasketRepository _basketRepository, IMapper _mapper) : IRequestHandler<AddOrUpdateBasketCommand, BasketDto>
    {
        public async Task<BasketDto> Handle(AddOrUpdateBasketCommand request, CancellationToken cancellationToken)
        {
            var basket=_mapper.Map<Basket>(request.basketDto);
            var createOrUpdateBasket=await _basketRepository.CreateOrUpdateBasket(basket);
            if (basket is not null)
                return await _basketRepository.GetBasketAsync(basket.Id.ToString()) is Basket createdBasket
                    ? _mapper.Map<BasketDto>(createdBasket)
                    : throw new Exception("Failed to retrieve the created or updated basket.");
            throw new Exception("Failed to create or update the basket.");


        }
    }
}
