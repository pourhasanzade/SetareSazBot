using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class ButtonService : IButtonService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public ButtonService(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        private const string CacheKey = "ButtonList";

        private async Task<List<ButtonEntity>> GetButtonList()
        {
            return await _context.Buttons.ToListAsync();
        }

        public async Task<ButtonEntity> GetButton(string code)
        {
            var buttonList = await _cacheService.GetOrSet(CacheKey, GetButtonList);
            return buttonList.FirstOrDefault(x => x.Code == code);
        }

        public async Task<List<ButtonEntity>> GetButtonList(long stateId)
        {
            var buttonList = await _cacheService.GetOrSet(CacheKey, GetButtonList);
            return buttonList.Where(x => x.StateId == stateId).ToList();
        }
    }
}