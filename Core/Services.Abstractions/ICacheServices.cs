using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface ICacheServices
    {
        Task SetCacheValueAsync(string key, object value, TimeSpan expiration);
        Task<string?> GetCacheValueAsync(string key);
    }
}
