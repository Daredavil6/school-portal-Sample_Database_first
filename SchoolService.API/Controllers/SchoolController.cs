using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using SchoolService.API.Service;

namespace SchoolService.API.Controllers
{
    /// <summary>
    /// Controller for managing School-related operations
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class SchoolController : ControllerBase
    {
        private readonly ISchoolService _schoolService;

        /// <summary>
        /// Initializes a new instance of the SchoolController
        /// </summary>
        /// <param name="schoolService">The School service for handling business logic</param>
        public SchoolController(ISchoolService schoolService)
        {
            _schoolService = schoolService;
        }

        /// <summary>
        /// Gets all schools
        /// </summary>
        /// <returns>List of all schools</returns>
        /// <response code="200">Returns the list of schools</response>
        [HttpGet("Schools")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<SchoolMaster>>> GetSchools()
        {
            return Ok(await _schoolService.GetAllAsync());
        }

        /// <summary>
        /// Gets a School by its ID
        /// </summary>
        /// <param name="id">The School ID</param>
        /// <returns>The School details</returns>
        /// <response code="200">Returns the School</response>
        /// <response code="404">If the School is not found</response>
        [HttpGet("School/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<SchoolMaster>> GetSchoolById(Guid id)
        {
            var School = await _schoolService.GetByIdAsync(id);

            if (School == null)
                return NotFound($"School with ID {id} not found.");

            return Ok(School);
        }

        /// <summary>
        /// Creates a new School
        /// </summary>
        /// <param name="dto">The School creation data</param>
        /// <returns>The created School</returns>
        /// <response code="200">Returns the created School</response>
        /// <response code="400">If the data is invalid</response>
        [HttpPost("School")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<SchoolMaster>> CreateSchool([FromBody] CreateSchoolDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var createdSchool = await _schoolService.AddAsync(dto);
            return Ok(createdSchool);
        }

        /// <summary>
        /// Updates an existing School
        /// </summary>
        /// <param name="id">The School ID</param>
        /// <param name="dto">The School update data</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the School was updated</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="404">If the School is not found</response>
        [HttpPut("School/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateSchool(Guid id, [FromBody] UpdateSchoolDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var updated = await _schoolService.UpdateAsync(id, dto);

            if (updated)
                return NoContent();

            return NotFound($"School with ID {id} not found.");
        }

        /// <summary>
        /// Deletes a School
        /// </summary>
        /// <param name="id">The School ID</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the School was deleted</response>
        /// <response code="404">If the School is not found</response>
        [HttpDelete("School/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteSchool(Guid id)
        {
            var deleted = await _schoolService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound($"School with ID {id} not found.");
        }
    }
}
