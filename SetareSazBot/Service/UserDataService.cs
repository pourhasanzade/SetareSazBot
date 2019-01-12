using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class UserDataService : IUserDataService
    {
        private readonly ApplicationDbContext _context;

        public UserDataService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserDataEntity> GetUserData(string chatId)
        {
            return await _context.UserData.Where(x => x.ChatId == chatId).FirstOrDefaultAsync();
        }

        public async Task<(UserDataEntity userData, bool isNew)> Update(string chatId, long stateId)
        {
            var userDataInDb = await _context.UserData.Where(x => x.ChatId == chatId).FirstOrDefaultAsync();
            if (userDataInDb == null)
            {
                var userData = new UserDataEntity
                {
                    ChatId = chatId,
                    StateId = 1
                };
                _context.UserData.Add(userData);
                await _context.SaveChangesAsync();
                return (userData, true);
            }
            else
            {
                if (stateId != 0) userDataInDb.StateId = stateId;
                await _context.SaveChangesAsync();
                return (userDataInDb, false);
            }

        }
    }
}