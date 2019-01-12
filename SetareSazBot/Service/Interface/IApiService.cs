using System.Collections.Generic;
using System.Threading.Tasks;
using SetareSazBot.API.Json.Output;

namespace SetareSazBot.Service.Interface
{
    public interface IApiService
    {
        Task<T> GetAsync<T>(string url, Dictionary<string, string> headers = null) where T : class;
        Task<T> PostAsync<T>(string url, object body = null, Dictionary<string, string> headers = null) where T : class;
        Task<T> PutAsync<T>(string url, object body = null, Dictionary<string, string> headers = null) where T : class;
        Task<bool> Download(string url, string fileName);
        Task<UploadOutput> Upload(string filePath, string url, Dictionary<string, string> headers);
    }
}
