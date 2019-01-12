using System.Collections.Generic;
using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Domain.Enum;

namespace SetareSazBot.Service.Interface
{
    public interface IUserInfoService
    {
        Task<UserInfoEntity> GetUserInfo(string chatId);

        Task<UserInfoEntity> UpdateFirsName(string chatId, string input);

        Task<UserInfoEntity> UpdateLastName(string chatId, string input);

        Task<UserInfoEntity> UpdateProvince(string chatId, long id, string input);

        Task<UserInfoEntity> UpdateCity(string chatId, long id, string input);

        Task<UserInfoEntity> UpdateMobile(string chatId, string input);

        Task<UserInfoEntity> UpdatePopulationStatus(string chatId, PopulationStatusEnum status, string input);

        Task<UserInfoEntity> UpdateAddress(string chatId, string input);

        Task<UserInfoEntity> UpdatePostalCode(string chatId, string input);

        Task<UserInfoEntity> UpdateBirthDate(string chatId, string input);

        Task<UserInfoEntity> UpdateNationalCode(string chatId, string input);

        Task<UserInfoEntity> UpdatePosition(string chatId, PositionTypeEnum type, string input);
        Task<UserInfoEntity> UpdateVideo(string chatId, string input);
    }
}