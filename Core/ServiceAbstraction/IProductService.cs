using Shared;
using Shared.DataTransferObject.ProductDtos;

namespace ServiceAbstraction
{
    public interface IProductService
    {

        Task<PaginationResult<ProductDto>> GetAllProductsAsync(ProductQueryParams productQueryParams);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();



    }
}
