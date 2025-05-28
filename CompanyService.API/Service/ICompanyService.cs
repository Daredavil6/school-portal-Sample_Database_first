using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Service;

namespace CompanyService.API.Service
{
    public interface ICompanyService : IGenericService<ViewCompanyDto, CreateCompanyDto, UpdateCompanyDto>
    {
        // Add any country-specific service methods here
    }
}