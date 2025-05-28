using AutoMapper;
using CountryService.API.Repository;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models;
using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Service;
using System.Diagnostics.Metrics;

namespace CountryService.API.Service
{
	public class CountryService : ICountryService
	{
		private readonly ICountryRepository _countryRepository;
		private readonly IMapper _mapper;

		public CountryService(ICountryRepository countryRepository, IMapper mapper)
		{
			_countryRepository = countryRepository;
			_mapper = mapper;
		}

		public async Task<ViewCountryDto?> AddAsync(CreateCountryDto dto)
		{
			var country = _mapper.Map<CountryMaster>(dto);
			country.CreatedDate = DateTime.UtcNow;
			country.ModifiedDate = null;
			country.ModifiedBy = null;
			await _countryRepository.CreateAsync(country);
			return _mapper.Map<ViewCountryDto?>(country);
		}

		public async Task<bool> DeleteAsync(Guid id)
		{
			return await DeleteAsync(id, new Guid("368BD1DF-42D6-4F05-9CA0-8DF22191B917")); // Default system user
		}

		public async Task<bool> DeleteAsync(Guid id, Guid modifiedBy)
		{
			var country = await _countryRepository.GetByIdAsync(id);
			if (country == null) return false;

			country.IsDeleted = true;
			country.IsActive = false;
			country.ModifiedDate = DateTime.UtcNow;
			country.ModifiedBy = modifiedBy;
			country.Status = "Deleted";
			country.StatusMessage = "Country Deleted Successfully";

			return await _countryRepository.UpdateAsync(id, country);
		}

		public async Task<IEnumerable<ViewCountryDto?>> GetAllAsync()
		{
			var countries = await _countryRepository.GetAllAsync();
			return _mapper.Map<IEnumerable<ViewCountryDto?>>(countries);
		}

		public async Task<UpdateCountryDto?> GetByIdAsync(Guid id)
		{
			var country = await _countryRepository.GetByIdAsync(id);
			return country == null ? null : _mapper.Map<UpdateCountryDto?>(country);
		}

		public async Task<bool> UpdateAsync(Guid id, UpdateCountryDto dto)
		{
			var country = await _countryRepository.GetByIdAsync(id);
			if (country == null) return false;

			_mapper.Map(dto, country);
			country.ModifiedDate = DateTime.UtcNow;
			country.ModifiedBy = dto.ModifiedBy;
			return await _countryRepository.UpdateAsync(id, country);
		}
	}
}
