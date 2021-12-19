using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace PucMinas.TCC.Domain.Services
{
    public class DistributedCacheService
    {
        readonly IDistributedCache DistributedCache;

        public DistributedCacheService(IDistributedCache distributedCache)
        {
            DistributedCache = distributedCache;
        }

        public async Task SaveInCache(string key, object value, TimeSpan? timeToExpire = null)
        {
            string content = JsonConvert.SerializeObject(value);

            if (timeToExpire.HasValue)
            {
                await DistributedCache.SetStringAsync(key, content, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = timeToExpire
                });

                return;
            }

            await DistributedCache.SetStringAsync(key, content);
        }

        public async Task<T> LoadByCache<T>(string key)
        {
            string content = await DistributedCache.GetStringAsync(key);

            if (!string.IsNullOrWhiteSpace(content))
                return JsonConvert.DeserializeObject<T>(content);

            return default;
        }
    }
}
