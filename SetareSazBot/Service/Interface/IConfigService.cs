using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IConfigService
    {
        Task UpdateLastMessageId(string messageId);
        Task<ConfigEntity> GetConfig();
    }
}