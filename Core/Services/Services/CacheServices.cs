using Domain.Contracts;
using Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class CacheServices(ICacheReposiotry cacheRepository) : ICacheServices
    {
        private readonly ICacheReposiotry _cacheRepository = cacheRepository;

        public async Task<string?> GetCacheValueAsync(string key)
        {
           var result = await _cacheRepository.GetAsync(key);
            return result == null ? null : result;
        }

        public async Task SetCacheValueAsync(string key, object value, TimeSpan expiration)
        {
             await _cacheRepository.SetAsync(key, value, expiration);
        }
    }
}
