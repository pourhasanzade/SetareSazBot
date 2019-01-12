using System.Data.Entity;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Domain.Enum;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class UserInfoService : IUserInfoService
    {
        private readonly ApplicationDbContext _context;

        public UserInfoService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserInfoEntity> GetUserInfo(string chatId)
        {
            var userInfoInDb = await _context.UserInfo.FirstOrDefaultAsync(x => x.ChatId == chatId && !x.Submitted);
            if (userInfoInDb != null) return userInfoInDb;

            var info = new UserInfoEntity
            {
                ChatId = chatId
            };
            _context.UserInfo.Add(info);
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateFirsName(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.FirstName = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateLastName(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.LastName = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateProvince(string chatId, long id, string input)
        {
            var info = await GetUserInfo(chatId);
            info.ProvinceId = id;
            info.Province = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateCity(string chatId, long id, string input)
        {
            var info = await GetUserInfo(chatId);
            info.CityId = id;
            info.City = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateMobile(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.Mobile = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdatePopulationStatus(string chatId, PopulationStatusEnum type, string input)
        {
            var info = await GetUserInfo(chatId);
            info.Resident = input;
            info.PopulationStatus = type;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateAddress(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.Address = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdatePostalCode(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.PostalCode = input;
            await _context.SaveChangesAsync();
            return info;
        }


        public async Task<UserInfoEntity> UpdateBirthDate(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.BirthDate = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateNationalCode(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.NationalCode = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdatePosition(string chatId, PositionTypeEnum type, string input)
        {
            var info = await GetUserInfo(chatId);
            info.PositionType = type;
            info.FavoritePost = input;
            await _context.SaveChangesAsync();
            return info;
        }

        public async Task<UserInfoEntity> UpdateVideo(string chatId, string input)
        {
            var info = await GetUserInfo(chatId);
            info.VideoSrc = input;
            await _context.SaveChangesAsync();
            return info;
        }

    }
}