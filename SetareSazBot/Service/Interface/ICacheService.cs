using System;
using System.Threading.Tasks;

namespace SetareSazBot.Service.Interface
{
    public interface ICacheService
    {
        Task<T> GetOrSet<T>(string cacheKey, Func<Task<T>> getItemCallback) where T : class;
        Task<T> GetOrSet<T>(string cacheKey, int minutes, Func<Task<T>> getItemCallback) where T : class;
        void Clear(string cacheKey);
    }
}
