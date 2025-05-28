using CompanyService.API.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;

namespace CompanyService.API.Controllers
{
    /// <summary>
    /// Controller for managing company-related operations
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _CompanyService;

        /// <summary>
        /// Initializes a new instance of the CompanyController
        /// </summary>
        /// <param name="CompanyService">The company service for handling business logic</param>
        public CompanyController(ICompanyService CompanyService)
        {
            _CompanyService = CompanyService;
        }

        /// <summary>
        /// Gets all companies
        /// </summary>
        /// <returns>List of all companies</returns>
        /// <response code="200">Returns the list of companies</response>
        [HttpGet("Companies")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<CompanyMaster>>> GetCompanies()
        {
            return Ok(await _CompanyService.GetAllAsync());
        }

        /// <summary>
        /// Gets a company by its ID
        /// </summary>
        /// <param name="id">The company ID</param>
        /// <returns>The company details</returns>
        /// <response code="200">Returns the company</response>
        /// <response code="404">If the company is not found</response>
        [HttpGet("Company/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CompanyMaster>> GetCompanyById(Guid id)
        {
            var Company = await _CompanyService.GetByIdAsync(id);

            if (Company == null)
                return NotFound($"Company with ID {id} not found.");

            return Ok(Company);
        }

        /// <summary>
        /// Creates a new company
        /// </summary>
        /// <param name="dto">The company creation data</param>
        /// <returns>The created company</returns>
        /// <response code="200">Returns the created company</response>
        /// <response code="400">If the data is invalid</response>
        [HttpPost("Company")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CompanyMaster>> CreateCompany([FromBody] CreateCompanyDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var createdCompany = await _CompanyService.AddAsync(dto);
            return Ok(createdCompany);
        }

        /// <summary>
        /// Updates an existing company
        /// </summary>
        /// <param name="id">The company ID</param>
        /// <param name="dto">The company update data</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the company was updated</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="404">If the company is not found</response>
        [HttpPut("Company/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateCompany(Guid id, [FromBody] UpdateCompanyDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var updated = await _CompanyService.UpdateAsync(id, dto);

            if (updated)
                return NoContent();

            return NotFound($"Company with ID {id} not found.");
        }

        /// <summary>
        /// Deletes a company
        /// </summary>
        /// <param name="id">The company ID</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the company was deleted</response>
        /// <response code="404">If the company is not found</response>
        [HttpDelete("Company/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteCompany(Guid id)
        {
            var deleted = await _CompanyService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound($"Company with ID {id} not found.");
        }
    }
}
