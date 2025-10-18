using Shared.DataTransferObject;

namespace ServiceAbstraction
{
    public interface IProductService
    {

        Task<IEnumerable<ProductDto>> GetAllProductsAsync(int? brandId, int? typeId);
        Task<ProductDto> GetProductByIdAsync(int id);
        Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
        Task<IEnumerable<TypeDto>> GetAllTypesAsync();



    }
}
