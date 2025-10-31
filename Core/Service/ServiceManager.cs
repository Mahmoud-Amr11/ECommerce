using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using MediatR;
using Microsoft.AspNetCore.Identity;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _unitOfWork,IMapper _mapper,IMediator mediator,UserManager<ApplicationUser> _userManager) : IServiceManager
    {
        private readonly Lazy<IProductService> _productService=new Lazy<IProductService>(()=> new ProductService(_unitOfWork,_mapper));
        private readonly Lazy<IBasketService> _basketService=new Lazy<IBasketService>(()=> new BasketService(mediator));
        private readonly Lazy<IAuthenticationService> _authenticationService=new Lazy<IAuthenticationService>(()=> new AuthenticationService(_userManager));

        public IProductService ProductService => _productService.Value;

        public IBasketService BasketService => _basketService.Value;

        public IAuthenticationService AuthenticationService => _authenticationService.Value;
    }
}
