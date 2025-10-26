using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using MediatR;
using Shared.DataTransferObject.BasketDtos;

namespace Service.BasketFeatures.Query
{
    public class GetBasketQueryHandler(IBasketRepository _basketRepository,IMapper _mapper) : IRequestHandler<GetBasketQuery, BasketDto?>
    {
        public async Task<BasketDto?> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
          var basket= await _basketRepository.GetBasketAsync(request.Id);
            if(basket is null)
                throw new BasketNotFoundException(request.Id);

            return _mapper.Map<BasketDto>(basket);

        }
    }
}
