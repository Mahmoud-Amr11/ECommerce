using DomainLayer.Contracts;
using ServiceAbstraction;

namespace Service
{
    internal class ServiceManagerWithFactoryDelegate(Func<IProductService> productFactory
        ,Func<IBasketService> basketFactory
        ,Func<IAuthenticationService> authFactory
        ,Func<IOrderService> orderFactory

        ) 
    {
        public IProductService ProductService => productFactory.Invoke();

        public IBasketService BasketService => basketFactory.Invoke();

        public IAuthenticationService AuthenticationService => authFactory.Invoke();

        public IOrderService OrderService => orderFactory.Invoke();
    }
}
