using Shared.DataTransferObject.BasketDtos;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId);
        Task UpdateOrderPaymentStatusAsync(string request,string stripeHeader);
    }
}
