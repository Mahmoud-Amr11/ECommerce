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
        public ProductWithBrandAndTypeSpecification()
            : base(null)
        {
            AddInclude(p => p.ProductType);
            AddInclude(p => p.ProductBrand);
        }
    }
}
