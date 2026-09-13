using Domain.Contracts;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Presistance.Data.Reposiotries
{
    public class CacheReposiotry(IConnectionMultiplexer connection) : ICacheReposiotry
    {
        private readonly IDatabase _database = connection.GetDatabase();
        public async Task<string?> GetAsync(string key)
        {
            var result = await _database.StringGetAsync(key);
            return result.IsNullOrEmpty ? default : result;

        }

        public async Task SetAsync(string key, object value, TimeSpan expiration)
        {
            var result = JsonSerializer.Serialize(value);

            await _database.StringSetAsync(key, result, expiration);
        }
    }
}
