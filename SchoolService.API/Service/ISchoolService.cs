using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Service;

namespace SchoolService.API.Service
{
    public interface ISchoolService : IGenericService<ViewSchoolDto, CreateSchoolDto, UpdateSchoolDto>
    {
        // Add any school-specific service methods here
    }
}
