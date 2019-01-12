using System;
using System.Runtime.Caching;
using System.Threading.Tasks;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class CacheService : ICacheService
    {
        public async Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class
        {
            return await GetOrSet<T>(cacheKey, 30, getItemCallback);
        }

        public async Task<T> GetOrSet<T>(string cacheKey, int minutes, Func<Task<T>> getItemCallback) where T : class
        {
            if (MemoryCache.Default.Get(cacheKey) is T item) return item;

            item = await getItemCallback();
            if (item != null)
            {
                MemoryCache.Default.Add(cacheKey, item, DateTime.Now.AddMinutes(minutes));
            }
            return item;
        }

        public void Clear(string cacheKey)
        {
            MemoryCache.Default.Remove(cacheKey);
        }
    }
}
