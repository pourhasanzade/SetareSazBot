using System.Collections.Generic;
using System.Threading.Tasks;
using SetareSazBot.Domain.Entity;

namespace SetareSazBot.Service.Interface
{
    public interface IProvinceService
    {
        Task<List<ProvinceEntity>> GetProvinceListAsync(int first, int last);
        Task<ProvinceEntity> GetProvinceAsync(string province);
        Task<int> GetProvinceCountAsync();
        Task<List<ProvinceEntity>> SearchProvinceAsync(string searchText, int limit);
        Task<List<CityEntity>> GetCityListAsync(long provinceId, int first, int last);
        Task<CityEntity> GetCityAsync(long provinceId, string city);
        Task<int> GetCityCountAsync(long provinceId);
        Task<List<CityEntity>> SearchCityAsync(long provinceId, string searchText, int limit);
    }
}
