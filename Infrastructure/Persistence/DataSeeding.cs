using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _context) : IDataSeeding
    {
        public async Task SeedDataAsync()
        {
            if((await  _context.Database.GetPendingMigrationsAsync()).Any())
            {
                _context.Database.Migrate();
            }

            if(!_context.ProductBrands.Any())
            {
                var brandsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedData\brands.json");
                var brands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(brandsData);
                if(brands != null && brands.Any())
                {
                  await  _context.ProductBrands.AddRangeAsync(brands);
                 
                }
            }
            if(!_context.ProductTypes.Any())
            {
                var typesData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedData\types.json");
                var types =await JsonSerializer.DeserializeAsync<List<ProductType>>(typesData);
                if(types != null && types.Any())
                {
                   await  _context.ProductTypes.AddRangeAsync(types);
                 
                }
            }
            if(!_context.Products.Any())
            {
                var productsData = File.OpenRead(@"..\Infrastructure\Persistence\Data\SeedData\products.json");
                var products =await JsonSerializer.DeserializeAsync<List<Product>>(productsData);
                if(products != null && products.Any())
                {
                    await   _context.Products.AddRangeAsync(products);
                 
                }
            }

           await  _context.SaveChangesAsync();
        }
    }
}
