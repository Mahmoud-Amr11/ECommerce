
namespace DomainLayer.Contracts
{
    public interface IDataSeeding
    {
        Task SeedDataAsync();
        Task IdentitySeedDataAsync();
    }
}
