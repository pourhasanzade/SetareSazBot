using System.Collections.Generic;
using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IButtonService
    {
        Task<ButtonEntity> GetButton(string code);
        Task<List<ButtonEntity>> GetButtonList(long stateId);
    }
}