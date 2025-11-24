using DomainLayer.Contracts;
using ServiceAbstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Service
{
    public class CacheService(ICacheRepository _CacheRepository) : ICacheService
    {
        public async Task<string?> GetCacheAsync(string key)
        {
            return await _CacheRepository.GetAsync(key);
        }
        public async Task SetCacheAsync(string key, object value, TimeSpan timeToive)
        {
           var CacheValue = JsonSerializer.Serialize(value);
             await _CacheRepository.SetAsync(key, CacheValue, timeToive);
        }

     
    }
}
