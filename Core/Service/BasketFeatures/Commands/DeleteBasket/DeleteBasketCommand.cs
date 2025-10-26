using MediatR;

namespace Service.BasketFeatures.Commands.DeleteBasket
{
    public record DeleteBasketCommand(string Id): IRequest<bool>
    {
    }
}
