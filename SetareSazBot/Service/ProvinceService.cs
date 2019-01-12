using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using SetareSazBot.DAL;
using SetareSazBot.Domain.Entity;
using SetareSazBot.Service.Interface;

namespace SetareSazBot.Service
{
    public class ProvinceService : IProvinceService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICacheService _cacheService;

        public ProvinceService(ApplicationDbContext context, ICacheService cacheService)
        {
            _context = context;
            _cacheService = cacheService;
        }

        #region Province

        private const string ProvinceCacheKey = "ProvinceList";

        private async Task<List<ProvinceEntity>> GetProvinceListAsync()
        {
            return await _context.Provinces.ToListAsync();
        }
        
        public async Task<List<ProvinceEntity>> GetProvinceListAsync(int first, int last)
        {
            var provinces = await _cacheService.GetOrSet(ProvinceCacheKey, GetProvinceListAsync);
            return provinces.OrderBy(x => x.Name).Skip(first).Take(last - first).ToList();
        }

        public async Task<ProvinceEntity> GetProvinceAsync(string province)
        {
            var provinces = await _cacheService.GetOrSet(ProvinceCacheKey, GetProvinceListAsync);
            return provinces.FirstOrDefault(x => x.Name == province);
        }

        public async Task<int> GetProvinceCountAsync()
        {
            var provinces = await _cacheService.GetOrSet(ProvinceCacheKey, GetProvinceListAsync);
            return provinces.Count();
        }

        public async Task<List<ProvinceEntity>> SearchProvinceAsync(string searchText, int limit)
        {
            var provinces = await _cacheService.GetOrSet(ProvinceCacheKey, GetProvinceListAsync);
            return provinces.Where(x => x.Name.Contains(searchText)).Take(limit).ToList();
        }

        #endregion

        #region City

        private const string CityCacheKey = "CityList";


        private async Task<List<CityEntity>> GetCityListAsync()
        {
            return await _context.Cities.ToListAsync();
        }

        public async Task<List<CityEntity>> GetCityListAsync(long provinceId, int first, int last)
        {
            var cities = await _cacheService.GetOrSet(CityCacheKey, GetCityListAsync);
            return cities.OrderBy(x => x.Level).ThenBy(x => x.Name).Where(x => x.ProvinceId == provinceId).Skip(first).Take(last - first).ToList();
        }

        public async Task<CityEntity> GetCityAsync(long provinceId, string city)
        {
            var cities = await _cacheService.GetOrSet(CityCacheKey, GetCityListAsync);
            return cities.FirstOrDefault(x => x.ProvinceId == provinceId && x.Name == city);
        }

        public async Task<int> GetCityCountAsync(long provinceId)
        {
            var cities = await _cacheService.GetOrSet(CityCacheKey, GetCityListAsync);
            return cities.Count(x => x.ProvinceId == provinceId);
        }

        public async Task<List<CityEntity>> SearchCityAsync(long provinceId, string searchText, int limit)
        {
            var cities = await _cacheService.GetOrSet(CityCacheKey, GetCityListAsync);
            return cities.Where(x => x.ProvinceId == provinceId && x.Name.Contains(searchText)).Take(limit).ToList();
        }

        #endregion
    }
}