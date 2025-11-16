using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModels;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Persistence.Data.Contexts;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence
{
    public class DataSeeding(StoreDbContext _context
        ,UserManager<ApplicationUser> _userManager
        ,RoleManager<IdentityRole> _roleManager) : IDataSeeding
    {
        public async Task IdentitySeedDataAsync()
        {
            try
            {
                if (!_roleManager.Roles.Any())
                {
                    var roles = new List<IdentityRole>
                {
                    new IdentityRole{Name="SuperAdmin"},
                    new IdentityRole{Name="Admin"}
                };
                    foreach (var role in roles)
                    {
                        _roleManager.CreateAsync(role).Wait();
                    }
                }
                if (!_userManager.Users.Any())
                {
                    var user1 = new ApplicationUser
                    {
                        UserName = "superAdmin",
                        Email = "superAdmin@gmail.com",
                        DisplayName = "Super Admin",
                        PhoneNumber = "1234567890",



                    };
                    var user2 = new ApplicationUser
                    {
                        UserName = "Admin",
                        Email = "Admin@gmail.com",
                        DisplayName = "Admin",
                        PhoneNumber = "0123456789"

                    };
                    await _userManager.CreateAsync(user1, "Pa$$w0rd");
                    await _userManager.CreateAsync(user2, "Pa$$w0rd");

                    await _userManager.AddToRoleAsync(user1, "SuperAdmin");
                    await _userManager.AddToRoleAsync(user2, "Admin");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            }
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
