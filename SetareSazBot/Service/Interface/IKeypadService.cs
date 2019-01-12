using System.Threading.Tasks;
using SetareSazBot.Domain.Model;

namespace SetareSazBot.Service.Interface
{
    public interface IKeypadService
    {
        Task<KeypadModel> GenerateButtonsAsync(long stateId, object myObject = null);
    }
}
