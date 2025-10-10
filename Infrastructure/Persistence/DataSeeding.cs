using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Text.Json;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _context) : IDataSeeding
    {
        public void SeedData()
        {
            if(!_context.Database.GetPendingMigrations().Any())
            {
                _context.Database.Migrate();
            }

            if(!_context.ProductBrands.Any())
            {
                var brandsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedData\brands.json");
                var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);
                if(brands != null && brands.Any())
                {
                    _context.ProductBrands.AddRange(brands);
                 
                }
            }
            if(!_context.ProductTypes.Any())
            {
                var typesData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedData\types.json");
                var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);
                if(types != null && types.Any())
                {
                    _context.ProductTypes.AddRange(types);
                 
                }
            }
            if(!_context.Products.Any())
            {
                var productsData = File.ReadAllText(@"..\Infrastructure\Persistence\Data\SeedData\products.json");
                var products = JsonSerializer.Deserialize<List<Product>>(productsData);
                if(products != null && products.Any())
                {
                    _context.Products.AddRange(products);
                 
                }
            }

            _context.SaveChanges();
        }
    }
}
