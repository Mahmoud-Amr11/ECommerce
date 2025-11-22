using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.BasketModule;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.OrderModules;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared.DataTransferObject.IdentityDto;
using Shared.DataTransferObject.OrderDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    internal class OrderService(IMapper _mapper,IBasketRepository _basketRepository,IUnitOfWork _unitOfWork) : IOrderService
    {
        public async Task<OrderToReturnDto> CreateOrderAsync(string userEmail, OrderDto orderDto)
        {
            var orderAddress= _mapper.Map<OrderAddress>(orderDto.Address);
            var basket=await _basketRepository.GetBasketAsync(orderDto.BasketId);

            if(basket is null)
                throw new BasketNotFoundException(orderDto.BasketId);

            List<OrderItem> orderItems=new List<OrderItem>();
            var product = _unitOfWork.GetRepository<Product, int>();
            foreach (var item in basket.BasketItems)
            {
                var productItem=await product.GetByIdAsync(item.Id);
                if (productItem is null)
                    throw new ProductNotFoundException(item.Id);

                var orderitem=new OrderItem
                {
                    Product=new ProductItemOrder()
                    {
                        ProductId=productItem.ID,
                        ProductName=productItem.Name,
                        PictureUrl=productItem.PictureUrl
                    },
                    Price=productItem.Price,
                    Quantity=item.Quantity
                };
                orderItems.Add(orderitem);
            }

            var deliveryMethod=await _unitOfWork.GetRepository<DeliveryMethod, int>().GetByIdAsync(orderDto.DelivrryMethodId);
            if(deliveryMethod is null)
                throw new DeliveryMethodNotFoundException(orderDto.DelivrryMethodId);

            var subtotal=orderItems.Sum(item=>item.Price * item.Quantity);

            var order = new Order(userEmail, orderAddress, deliveryMethod,orderItems, subtotal);
            

           await _unitOfWork.GetRepository<Order,Guid>().AddAsync(order);
          
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<OrderToReturnDto>(order);
        }

        public async Task<IEnumerable<OrderToReturnDto>> GetAllOrdersAsync(string userEmail)
        {
             var spec=new OrderSpecification(userEmail);
             var orders=await _unitOfWork.GetRepository<Order,Guid>().GetAllAsync(spec);
            return _mapper.Map<IEnumerable<OrderToReturnDto>>(orders);
        }

        public async Task<IEnumerable<DeliveryMethodDto>> GetDeliveryMethodsAsync()
        {
            var deliveryMethods=await _unitOfWork.GetRepository<DeliveryMethod,int>().GetAllAsync();
            return _mapper.Map<IEnumerable<DeliveryMethodDto>>(deliveryMethods);
        }

        public async Task<OrderToReturnDto> GetOrderByIdAsync(Guid id)
        {
            var spec=new OrderSpecification(id);
            var order=await _unitOfWork.GetRepository<Order,Guid>().GetByIdAsync(spec);
            return _mapper.Map<OrderToReturnDto>(order);
        }
    }
}
