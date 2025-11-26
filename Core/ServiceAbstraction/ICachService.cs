using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceAbstraction
{
    public interface ICacheService
    {
        Task<string> GetCacheAsync(string key);
        Task SetCacheAsync(string key,object value ,TimeSpan timeToive);
    }
}
