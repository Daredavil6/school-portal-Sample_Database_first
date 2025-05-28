using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolPortal.Common.Service;

namespace CityService.API.Service
{
    public interface ICityService : IGenericService<ViewCityDto, CreateCityDto, UpdateCityDto>
    {
        // Add any city-specific service methods here
    }
}