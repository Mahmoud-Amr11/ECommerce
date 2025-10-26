using DomainLayer.Contracts;
using MediatR;

namespace Service.BasketFeatures.Commands.DeleteBasket
{
    public class DeleteBasketCommandHandler(IBasketRepository _basketRepository) : IRequestHandler<DeleteBasketCommand, bool>
    {
        public async Task<bool> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            return await _basketRepository.DeleteBasketAsync(request.Id);
        }
    }
}
