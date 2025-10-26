using MediatR;
using Shared.DataTransferObject.BasketDtos;

namespace Service.BasketFeatures.Query
{
    public record GetBasketQuery(string Id) : IRequest<BasketDto?>
    { }
}
