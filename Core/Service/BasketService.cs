using DomainLayer.Contracts;
using MediatR;
using Service.BasketFeatures.Commands.AddOrUpdateBasket;
using Service.BasketFeatures.Commands.DeleteBasket;
using Service.BasketFeatures.Query;
using Shared.DataTransferObject.BasketDtos;

namespace Service
{
    public class BasketService : IBasketService
    {
        private readonly IMediator _mediator;

        public BasketService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<BasketDto?> GetBasketAsync(string basketId)
        {
            var query = new GetBasketQuery(basketId);
            return await _mediator.Send(query);
        }

        public async Task<BasketDto> AddOrUpdateBasketAsync(BasketDto basket)
        {
            var command = new AddOrUpdateBasketCommand(basket);
            return await _mediator.Send(command);
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            var command = new DeleteBasketCommand(basketId);
            await _mediator.Send(command);
            return true;
        }
    }
}
