using DomainLayer.Models;

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
        public ProductWithBrandAndTypeSpecification(int? brandId, int? typeId)
            : base(p=> (!brandId.HasValue || p.BrandId==brandId)
            &&
            (!typeId.HasValue || p.TypeId==typeId))
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
