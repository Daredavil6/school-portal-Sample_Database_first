using AutoMapper;
using CompanyService.API.Repository;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;

namespace CompanyService.API.Service
{
    public class CompanyService : ICompanyService
    {
        private readonly ICompanyRepository _CompanyRepository;
        private readonly IMapper _mapper;

        public CompanyService(ICompanyRepository CompanyRepository, IMapper mapper)
        {
            _CompanyRepository = CompanyRepository;
            _mapper = mapper;
        }

        public async Task<ViewCompanyDto?> AddAsync(CreateCompanyDto dto)
        {
            var Company = _mapper.Map<CompanyMaster>(dto);
            await _CompanyRepository.CreateAsync(Company);
            return _mapper.Map<ViewCompanyDto?>(Company);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var success = await _CompanyRepository.DeleteAsync(id);
            if (!success)
            {
                throw new InvalidOperationException("Failed to delete Company");
            }
            return true;
        }

        public async Task<IEnumerable<ViewCompanyDto?>> GetAllAsync()
        {
            var companies = await _CompanyRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ViewCompanyDto?>>(companies);
        }

        public async Task<UpdateCompanyDto?> GetByIdAsync(Guid id)
        {
            var company = await _CompanyRepository.GetByIdAsync(id);
            return company == null ? null : _mapper.Map<UpdateCompanyDto?>(company);
        }

        public async Task<bool> UpdateAsync(Guid id, UpdateCompanyDto dto)
        {
            var company = await _CompanyRepository.GetByIdAsync(id);
            if (company == null) return false;

            _mapper.Map(dto, company);
            return await _CompanyRepository.UpdateAsync(id, company);
        }
    }
}