using AutoMapper;
using DomainLayer.Contracts;
using DomainLayer.Exceptions;
using DomainLayer.Models.ProductModule;
using Service.Specifications;
using ServiceAbstraction;
using Shared;
using Shared.DataTransferObject.ProductDtos;

namespace Service
{
    public class ProductService(IUnitOfWork _unitOfWork,IMapper _mapper) : IProductService
    {
 

        public async Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQueryParams)
        {
            var specification = new ProductWithBrandAndTypeSpecification( productQueryParams);
            var repo= _unitOfWork.GetRepository<Product,int>();
            var products=await repo.GetAllAsync(specification);
            var productsDto= _mapper.Map<IEnumerable<ProductDto>>(products);
            var productCount=productsDto.Count();
            var paginationResult = new PaginationResult<ProductDto>
            {
                PageIndex = productQueryParams.PageIndex,
                PageSize = productCount,
                TotalItems =0,
                Data = productsDto
            };
            return paginationResult;
        }
        public async Task<ProductDto?> GetProductByIdAsync(int id)
        {
            var specification = new ProductWithBrandAndTypeSpecification(id);
            var product=await  _unitOfWork.GetRepository<Product,int>().GetByIdAsync(specification);
            if(product is null)
                throw new ProductNotFoundException(id);
            return  product is null ? null : _mapper.Map<ProductDto>(product);
        }

        public async Task<IEnumerable<TypeDto>> GetAllTypesAsync()
        {
            var productTypes=await _unitOfWork.GetRepository<ProductType, int>().GetAllAsync();
            var productTypesDto = _mapper.Map<IEnumerable<TypeDto>>(productTypes);
            return productTypesDto;
        }



        public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
        {
            var productBrands=await _unitOfWork.GetRepository<ProductBrand, int>().GetAllAsync();
            var productBrandsDto = _mapper.Map<IEnumerable<BrandDto>>(productBrands);
            return productBrandsDto;
        }
    }
}
