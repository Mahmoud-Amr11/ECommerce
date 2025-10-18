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
        public ProductWithBrandAndTypeSpecification(int? brandId, int? typeId,ProductSortingOption? sortingOption)
            : base(p=> (!brandId.HasValue || p.BrandId==brandId)
            &&
            (!typeId.HasValue || p.TypeId==typeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);

            switch (sortingOption)
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
                    AddOrderBy(p => p.Name);
                    break;
            }
        }
    }
}
