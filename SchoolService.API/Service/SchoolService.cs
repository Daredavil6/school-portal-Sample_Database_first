using AutoMapper;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolService.API.Repository;

namespace SchoolService.API.Service
{
    public class SchoolService : ISchoolService
    {
        private readonly ISchoolRepository _schoolRepository;
        private readonly IMapper _mapper;

        public SchoolService(ISchoolRepository schoolRepository, IMapper mapper)
        {
            _schoolRepository = schoolRepository;
            _mapper = mapper;
        }

        public async Task<ViewSchoolDto?> AddAsync(CreateSchoolDto dto)
        {
            var School = _mapper.Map<SchoolMaster>(dto);
            await _schoolRepository.CreateAsync(School);
            return _mapper.Map<ViewSchoolDto?>(School);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var success = await _schoolRepository.DeleteAsync(id);
            if (!success)
            {
                throw new InvalidOperationException("Failed to delete School");
            }
            return true;
        }

        public async Task<IEnumerable<ViewSchoolDto?>> GetAllAsync()
        {
            var schools = await _schoolRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewSchoolDto?>>(schools);
        }

        public async Task<UpdateSchoolDto?> GetByIdAsync(Guid id)
        {
            var School = await _schoolRepository.GetByIdAsync(id);
            return School == null ? null : _mapper.Map<UpdateSchoolDto?>(School);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateSchoolDto dto)
        {
            var School = await _schoolRepository.GetByIdAsync(id);
            if (School == null) return false;

            _mapper.Map(dto, School);
            return await _schoolRepository.UpdateAsync(id, School);
        }
    }
}
