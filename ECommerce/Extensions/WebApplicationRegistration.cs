using DomainLayer.Contracts;
using System.Threading.Tasks;

namespace ECommerce.Web.Extensions
{
    public static class WebApplicationRegistration
    {
        public static async Task<WebApplication> SeedingData(this WebApplication app)
        {

            var Scope = app.Services.CreateScope();
            var dataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();


              await dataSeeding.SeedDataAsync();
              await dataSeeding.IdentitySeedDataAsync();
            return app;
        }
    }
}
