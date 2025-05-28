using AutoMapper;
using CityService.API.Repository;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;

namespace CityService.API.Service
{
    public class CityService : ICityService
    {
        private readonly ICityRepository _CityRepository;
        private readonly IMapper _mapper;

        public CityService(ICityRepository CityRepository, IMapper mapper)
        {
            _CityRepository = CityRepository;
            _mapper = mapper;
        }

        public async Task<ViewCityDto?> AddAsync(CreateCityDto dto)
        {
            var City = _mapper.Map<CityMaster>(dto);
            await _CityRepository.CreateAsync(City);
            return _mapper.Map<ViewCityDto?>(City);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var success = await _CityRepository.DeleteAsync(id);
            if (!success)
            {
                throw new InvalidOperationException("Failed to delete City");
            }
            return true;
        }

        public async Task<IEnumerable<ViewCityDto?>> GetAllAsync()
        {
            var cities = await _CityRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewCityDto?>>(cities);
        }

        public async Task<UpdateCityDto?> GetByIdAsync(Guid id)
        {
            var City = await _CityRepository.GetByIdAsync(id);
            return City == null ? null : _mapper.Map<UpdateCityDto?>(City);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCityDto dto)
        {
            var City = await _CityRepository.GetByIdAsync(id);
            if (City == null) return false;

            _mapper.Map(dto, City);
            return await _CityRepository.UpdateAsync(id, City);
        }
    }
}