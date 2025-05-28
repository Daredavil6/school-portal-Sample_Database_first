using CityService.API.Service;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;

namespace CityService.API.Controllers
{
    /// <summary>
    /// Controller for managing city-related operations
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CityController : ControllerBase
    {
        private readonly ICityService _CityService;

        /// <summary>
        /// Initializes a new instance of the CityController
        /// </summary>
        /// <param name="CityService">The city service for handling business logic</param>
        public CityController(ICityService CityService)
        {
            _CityService = CityService;
        }

        /// <summary>
        /// Gets all cities
        /// </summary>
        /// <returns>List of all cities</returns>
        /// <response code="200">Returns the list of cities</response>
        [HttpGet("countries")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ViewCityDto>>> GetCountries()
        {
            return Ok(await _CityService.GetAllAsync());
        }

        /// <summary>
        /// Gets a city by its ID
        /// </summary>
        /// <param name="id">The city ID</param>
        /// <returns>The city details</returns>
        /// <response code="200">Returns the city</response>
        /// <response code="404">If the city is not found</response>
        [HttpGet("City/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateCityDto>> GetCityById(Guid id)
        {
            var City = await _CityService.GetByIdAsync(id);

            if (City == null)
                return NotFound($"City with ID {id} not found.");

            return Ok(City);
        }

        /// <summary>
        /// Creates a new city
        /// </summary>
        /// <param name="dto">The city creation data</param>
        /// <returns>The created city</returns>
        /// <response code="200">Returns the created city</response>
        /// <response code="400">If the data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ViewCityDto>> CreateCity([FromBody] CreateCityDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var createdCity = await _CityService.AddAsync(dto);
            return Ok(createdCity);
        }

        /// <summary>
        /// Updates an existing city
        /// </summary>
        /// <param name="id">The city ID</param>
        /// <param name="dto">The city update data</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the city was updated</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="404">If the city is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCity(Guid id, [FromBody] UpdateCityDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var updated = await _CityService.UpdateAsync(id, dto);

            if (updated)
                return NoContent();

            return NotFound($"City with ID {id} not found.");
        }

        /// <summary>
        /// Deletes a city
        /// </summary>
        /// <param name="id">The city ID</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the city was deleted</response>
        /// <response code="404">If the city is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCity(Guid id)
        {
            var deleted = await _CityService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound($"City with ID {id} not found.");
        }
    }
}
