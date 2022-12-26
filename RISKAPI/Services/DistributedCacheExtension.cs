using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace RISKAPI.Services
{
    public static class DistributedCacheExtension
    {
        public static async Task SetRecordAsync<T>(this IDistributedCache cache, string recordId, T data, TimeSpan? absoluteTimeout = null, TimeSpan? unusedExpireTime = null)
        {
            DistributedCacheEntryOptions options = new DistributedCacheEntryOptions();
            options.AbsoluteExpirationRelativeToNow = absoluteTimeout ?? TimeSpan.FromSeconds(60);
            options.SlidingExpiration = unusedExpireTime;

            string jsonData = JsonConvert.SerializeObject(data);
            await cache.SetStringAsync(recordId, jsonData, options);
        }

        public static async Task<T> GetRecordAsync<T>(this IDistributedCache cache, string recordId)
        {
            string jsonData = await cache.GetStringAsync(recordId);

            return JsonConvert.DeserializeObject<T>(jsonData);
        }
    }
}
