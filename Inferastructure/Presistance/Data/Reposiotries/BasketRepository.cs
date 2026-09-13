using Domain.Contracts;
using Domain.Models;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace Presistance.Data.Reposiotries
{
    public class BasketRepository(IConnectionMultiplexer connection) : IBasketRepository
    {
        private readonly IDatabase _database = connection.GetDatabase();

        public async Task<CustomerBasket?> GetBasketAsync(string id)
        {
           var redisValue =  await _database.StringGetAsync(id);
            if (redisValue.IsNullOrEmpty) return null;
          var reslut =  JsonSerializer.Deserialize<CustomerBasket>(redisValue);
          return reslut != null ? reslut : null;
        }

        public async Task<CustomerBasket?> UpdateBasketAsync(CustomerBasket Basket, TimeSpan? TimeToLive = null)
        {
           var redisValue =  JsonSerializer.Serialize(Basket);
            
          var flag = await _database.StringSetAsync(Basket.Id, redisValue, TimeSpan.FromDays(30));
            return flag ? await GetBasketAsync(Basket.Id) : null;
        }


        public async Task<bool> DeleteBasketAsync(string id)
        {
           return await _database.KeyDeleteAsync(id);
        }

    
    }
}
