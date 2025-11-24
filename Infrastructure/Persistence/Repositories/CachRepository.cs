using DomainLayer.Contracts;
using StackExchange.Redis;

namespace Persistence.Repositories
{
    public class CacheRepository(IConnectionMultiplexer _connection) : ICacheRepository
    {
        private readonly IDatabase _database = _connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
           var CacheValue =await _database.StringGetAsync(key);
            return  CacheValue.IsNullOrEmpty ? CacheValue.ToString() : null;
        }

        public async Task SetAsync(string key, string value, TimeSpan timeToLive)
        {
           await  _database.StringSetAsync(key, value, timeToLive);
        }
    }
}
