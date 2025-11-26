using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModule;
using Microsoft.Extensions.Configuration;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObject.BasketDtos;
using Stripe;

namespace Service
{
    public class PaymentService(IConfiguration _configuration
        ,IUnitOfWork _unitOfWork
        ,IBasketRepository _basketRepository
        ,IMapper _mapper) : IPaymentService
    {
        public async Task<BasketDto> CreateOrUpdatePaymentIntentAsync(string basketId)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("stripe")["SecretKey"];
            var basket = await _basketRepository.GetBasketAsync(basketId);
            if (basket is null)
                throw new BasketNotFoundException(basketId);

            var productRepo =  _unitOfWork.GetRepository<DomainLayer.Models.ProductModule.Product, int>();
            foreach(var item in basket.BasketItems)
            {
                var product = await productRepo.GetByIdAsync(item.Id);
                if(product is null)
                    throw new ProductNotFoundException(item.Id);
                item.Price = product.Price;
                 
            }
            if (basket.DeliveryMethodId is null)
                throw new ArgumentException();


           
            var deliveryMethod= await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(basket.DeliveryMethodId.Value);


            if (deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(basket.DeliveryMethodId.Value);

            var shippingPrice = deliveryMethod.Price;
            var amount = (long)(basket.BasketItems.Sum(item => item.Quantity * (item.Price )) + (long)shippingPrice) * 100;

            var service = new PaymentIntentService();

            if(basket.PaymentIntendId is null)
            {
                var options = new PaymentIntentCreateOptions
                {
                    Amount = amount,
                    Currency = "usd",
                    PaymentMethodTypes = ["card"]
                };
                var intent = await service.CreateAsync(options);
                basket.PaymentIntendId = intent.Id;
                basket.ClientSecret = intent.ClientSecret;
            }
            else
            {
                var options = new PaymentIntentUpdateOptions
                {
                    Amount = amount
                };
                await service.UpdateAsync(basket.PaymentIntendId, options);
            }

            await _basketRepository.CreateOrUpdateBasket(basket);

            return _mapper.Map<BasketDto>(basket);
        }

        public async Task UpdateOrderPaymentStatusAsync(string request, string stripeHeader)
        {
            var endPointSecret = _configuration.GetSection("stripe")["EndpointSecret"];
            var stripeEvent = EventUtility.ConstructEvent(
                request,
                stripeHeader,
                endPointSecret
            );

            var paymentIntent = (PaymentIntent)stripeEvent.Data.Object;
            switch (stripeEvent.Type)
            {
                case EventTypes.PaymentIntentPaymentFailed:
                    await UpdatePaymentFaildedAsync(paymentIntent.Id);
                    break;
                case EventTypes.PaymentIntentSucceeded:
                    await UpdatePaymentRecievedAsync(paymentIntent.Id);
                    break;

                default:
                    Console.WriteLine($"Unhandled Stipe Event Type {stripeEvent.Type}");
                    break;

            }
        }
        private async Task UpdatePaymentRecievedAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<DomainLayer.Models.OrderModules.Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecification(paymentIntentId));
            order.OrderStatus = OrderStatus.PaymentReceived;
             
             orderRepo.Update(order);
             _unitOfWork.SaveChangesAsync();

        }
        private async Task UpdatePaymentFaildedAsync(string paymentIntentId)
        {
            var orderRepo = _unitOfWork.GetRepository<DomainLayer.Models.OrderModules.Order, Guid>();
            var order = await orderRepo.GetByIdAsync(new OrderWithPaymentIntentIdSpecification(paymentIntentId));
            order.OrderStatus = OrderStatus.PaymentFailed;

            orderRepo.Update(order);
            _unitOfWork.SaveChangesAsync();

        }
    }
}
