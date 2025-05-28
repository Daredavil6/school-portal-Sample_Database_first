using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Service;

namespace StateService.API.Service
{
    public interface IStateService : IGenericService<ViewStateDto, CreateStateDto, UpdateStateDto>
    {
        Task<bool> DeleteAsync(Guid id, Guid modifiedBy);
    }
}
