using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.dboSchema;
using StateService.API.Service;

namespace StateService.API.Controllers
{
    /// <summary>
    /// Controller for managing state-related operations
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class StateController : ControllerBase
    {
        private readonly IStateService _StateService;

        /// <summary>
        /// Initializes a new instance of the StateController
        /// </summary>
        /// <param name="StateService">The state service for handling business logic</param>
        public StateController(IStateService StateService)
        {
            _StateService = StateService;
        }

        /// <summary>
        /// Gets all states
        /// </summary>
        /// <returns>List of all states</returns>
        /// <response code="200">Returns the list of states</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ViewStateDto>>> GetStates()
        {
            return Ok(await _StateService.GetAllAsync());
        }

        /// <summary>
        /// Gets a state by its ID
        /// </summary>
        /// <param name="id">The state ID</param>
        /// <returns>The state details</returns>
        /// <response code="200">Returns the state</response>
        /// <response code="404">If the state is not found</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateStateDto>> GetStateById(Guid id)
        {
            var State = await _StateService.GetByIdAsync(id);

            if (State == null)
                return NotFound($"State with ID {id} not found.");

            return Ok(State);
        }

        /// <summary>
        /// Creates a new state
        /// </summary>
        /// <param name="dto">The state creation data</param>
        /// <returns>The created state</returns>
        /// <response code="200">Returns the created state</response>
        /// <response code="400">If the data is invalid</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ViewStateDto>> CreateState([FromBody] CreateStateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var createdState = await _StateService.AddAsync(dto);
            return Ok(createdState);
        }

        /// <summary>
        /// Updates an existing state
        /// </summary>
        /// <param name="id">The state ID</param>
        /// <param name="dto">The state update data</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the state was updated</response>
        /// <response code="400">If the data is invalid</response>
        /// <response code="404">If the state is not found</response>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateState(Guid id, [FromBody] UpdateStateDto dto)
        {
            if (dto == null)
                return BadRequest("Invalid data.");

            var updated = await _StateService.UpdateAsync(id, dto);

            if (updated)
                return NoContent();

            return NotFound($"State with ID {id} not found.");
        }

        /// <summary>
        /// Deletes a state
        /// </summary>
        /// <param name="id">The state ID</param>
        /// <returns>No content if successful</returns>
        /// <response code="204">If the state was deleted</response>
        /// <response code="404">If the state is not found</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteState(Guid id)
        {
            var deleted = await _StateService.DeleteAsync(id);

            if (deleted)
                return NoContent();

            return NotFound($"State with ID {id} not found.");
        }
    }
}
