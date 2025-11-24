namespace DomainLayer.Contracts
{
    public interface ICacheRepository
    {
        Task<string?> GetAsync(string key);
        Task SetAsync(string key, string value,TimeSpan timeToLive);


    }
}
