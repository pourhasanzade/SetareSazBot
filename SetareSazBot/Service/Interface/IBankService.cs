using System.Collections.Generic;
using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IBankService
    {
        Task<BankEntity> GetBank(string bank);
        Task<List<BankEntity>> SearchBank(string searchText, int limit);
        Task<int> GetBankCount();
        Task<List<BankEntity>> GetBankList(int firstIndex, int lastIndex);
    }
}