using DomainLayer.Contracts;
using DomainLayer.Models.BasketModule;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<Basket?> CreateOrUpdateBasket(Basket basket, TimeSpan? TimeToLive = null)
        {
            var created = await _database.StringSetAsync(
                            basket.Id.ToString(),
                            JsonSerializer.Serialize(basket),
                            TimeToLive ?? TimeSpan.FromDays(3)
                            );
            if (created)
                return await GetBasketAsync(basket.Id.ToString());
            return null;
        }

        public async Task<bool> DeleteBasketAsync(string basketId)
        {
            return _database.KeyDelete(basketId);
        }

        public async Task<Basket?> GetBasketAsync(string basketId)
        {
            var basket = _database.StringGet(basketId);
            if (basket.IsNullOrEmpty)
                return null;
            return JsonSerializer.Deserialize<Basket>(basket);
        }
    }
}
