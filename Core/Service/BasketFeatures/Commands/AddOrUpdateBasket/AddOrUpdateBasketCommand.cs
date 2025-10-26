using MediatR;
using Shared.DataTransferObject.BasketDtos;

namespace Service.BasketFeatures.Commands.AddOrUpdateBasket
{
    public record AddOrUpdateBasketCommand(BasketDto basketDto) :IRequest<BasketDto>
    {
    }
}
