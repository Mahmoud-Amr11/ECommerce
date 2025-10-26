using Shared.DataTransferObject.BasketDtos;

namespace DomainLayer.Contracts
{
    public interface IBasketService
    {
        Task<BasketDto?> GetBasketAsync(string basketId);
        Task<BasketDto> AddOrUpdateBasketAsync(BasketDto basket);
        Task<bool> DeleteBasketAsync(string basketId);
    }
}
