using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Repository;

namespace SchoolService.API.Repository
{
    public interface ISchoolRepository : IGenericRepository<SchoolMaster>
    {
        // Add any school-specific repository methods here
    }
}
