using DomainLayer.Models;
using Shared;

namespace Service.Specifications
{
     class ProductWithBrandAndTypeSpecification : BaseSpecification<Product, int>
    {
        //Get By Id including Brand and Type
        public ProductWithBrandAndTypeSpecification(int id)
            : base(p => p.ID == id)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }

        //Get All including Brand and Type
        public ProductWithBrandAndTypeSpecification(ProductQueryParams productQueryParams)
            : base(p => (!productQueryParams.brandId.HasValue || p.BrandId == productQueryParams.brandId.Value)
            &&
            (!productQueryParams.typeId.HasValue || p.TypeId == productQueryParams.typeId.Value)
            &&
            (string.IsNullOrWhiteSpace(productQueryParams.searchTerm) || p.Name.ToLower().Contains(productQueryParams.searchTerm.ToLower()))
            )
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (productQueryParams.sortingOption)
            {
                case ProductSortingOption.PriceAsc:
                    AddOrderBy(p => p.Price);
                    break;
                case ProductSortingOption.PriceDesc:
                    AddOrderByDescending(p => p.Price);
                    break;
                case ProductSortingOption.NameDesc:
                    AddOrderByDescending(p => p.Name);
                    break;
                case ProductSortingOption.NameAsc:
                    AddOrderBy(p => p.Name);
                    break;
                default:              
                    break;
            }

            ApplyPagination(productQueryParams.PageIndex, productQueryParams.PageSize);
        }
    }
}
