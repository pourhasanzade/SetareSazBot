using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IUserDataService
    {
        Task<UserDataEntity> GetUserData(string chatId);
        Task<(UserDataEntity userData, bool isNew)> Update(string chatId, long stateId);
    }
}