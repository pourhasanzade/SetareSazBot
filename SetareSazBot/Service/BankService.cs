using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class BankService : IBankService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public BankService(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        private const string CacheKey = "BankList";

        private async Task<List<BankEntity>> GetBankList()
        {
            return await _context.Banks.OrderBy(x=>x.Name).ToListAsync();
        }

        public async Task<BankEntity> GetBank(string bank)
        {
            var bankList = await _cacheService.GetOrSet(CacheKey, GetBankList);
            return bankList.FirstOrDefault(x => x.Name == bank);
        }

        public async Task<List<BankEntity>> SearchBank(string searchText, int limit)
        {
            var bankList = await _cacheService.GetOrSet(CacheKey, GetBankList);
            return bankList.Where(x => x.Name.Contains(searchText)).Take(limit).ToList();
        }

        public async Task<int> GetBankCount()
        {
            var bankList = await _cacheService.GetOrSet(CacheKey, GetBankList);
            return bankList.Count;
        }

        public async Task<List<BankEntity>> GetBankList(int firstIndex, int lastIndex)
        {
            var bankList = await _cacheService.GetOrSet(CacheKey, GetBankList);
            return bankList.Skip(firstIndex).Take(lastIndex - firstIndex).ToList();
        }
    }
}