using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SchoolPortal.Common.DTOs;
using SchoolPortal.Common.Models.CustomModels;
using SchoolPortal.Common.Models.dboSchema;
using UserDetailsService.API.Service;

namespace UserDetailsService.API.Controllers
{
	/// <summary>
	/// Controller for managing user details and authentication operations
	/// </summary>
	[Route("api/v1/[controller]")]
	[ApiController]
	public class UserDetailsController : ControllerBase
	{
		private readonly IUserDetailsService _userDetailsService;

		/// <summary>
		/// Initializes a new instance of the UserDetailsController
		/// </summary>
		/// <param name="userDetailsService">The user details service for handling business logic</param>
		public UserDetailsController(IUserDetailsService userDetailsService)
		{
			_userDetailsService = userDetailsService;
		}

		/// <summary>
		/// Gets all user details
		/// </summary>
		/// <returns>List of all user details</returns>
		/// <response code="200">Returns the list of user details</response>
		[HttpGet("userDetails")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		public async Task<ActionResult<IEnumerable<UserDetail>>> GetUserDetails()
		{
			return Ok(await _userDetailsService.GetAllAsync());
		}

		/// <summary>
		/// Gets a user detail by its ID
		/// </summary>
		/// <param name="id">The user detail ID</param>
		/// <returns>The user detail information</returns>
		/// <response code="200">Returns the user detail</response>
		/// <response code="404">If the user detail is not found</response>
		[HttpGet("userDetails/{id}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<ActionResult<UserDetail>> GetuserDetailsById(Guid id)
		{
			var userDetails = await _userDetailsService.GetByIdAsync(id);

			if (userDetails == null)
				return NotFound($"userDetails with ID {id} not found.");

			return Ok(userDetails);
		}

		/// <summary>
		/// Creates a new user detail
		/// </summary>
		/// <param name="dto">The user detail creation data</param>
		/// <returns>The created user detail</returns>
		/// <response code="200">Returns the created user detail</response>
		/// <response code="400">If the data is invalid</response>
		[HttpPost("userDetails")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		public async Task<ActionResult<UserDetail>> CreateuserDetails([FromBody] CreateUserDetailDto dto)
		{
			if (dto == null)
				return BadRequest("Invalid data.");

			var createduserDetails = await _userDetailsService.AddAsync(dto);
			return Ok(createduserDetails);
		}

		/// <summary>
		/// Updates an existing user detail
		/// </summary>
		/// <param name="id">The user detail ID</param>
		/// <param name="dto">The user detail update data</param>
		/// <returns>No content if successful</returns>
		/// <response code="204">If the user detail was updated</response>
		/// <response code="400">If the data is invalid</response>
		/// <response code="404">If the user detail is not found</response>
		[HttpPut("userDetails/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> UpdateuserDetails(Guid id, [FromBody] UpdateUserDetailDto dto)
		{
			if (dto == null)
				return BadRequest("Invalid data.");

			var updated = await _userDetailsService.UpdateAsync(id, dto);

			if (updated)
				return NoContent();

			return NotFound($"userDetails with ID {id} not found.");
		}

		/// <summary>
		/// Deletes a user detail
		/// </summary>
		/// <param name="id">The user detail ID</param>
		/// <returns>No content if successful</returns>
		/// <response code="204">If the user detail was deleted</response>
		/// <response code="404">If the user detail is not found</response>
		[HttpDelete("userDetails/{id}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> DeleteuserDetails(Guid id)
		{
			var deleted = await _userDetailsService.DeleteAsync(id);

			if (deleted)
				return NoContent();

			return NotFound($"userDetails with ID {id} not found.");
		}

		/// <summary>
		/// Authenticates a user with username and password
		/// </summary>
		/// <param name="login">The login credentials</param>
		/// <returns>Authentication result</returns>
		/// <response code="200">If authentication is successful</response>
		/// <response code="400">If credentials are missing</response>
		/// <response code="401">If credentials are invalid</response>
		[HttpPost("authenticate")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<IActionResult> AuthenticateUser([FromBody] LoginDto login)
		{
			if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.UserPassword))
				return BadRequest("Username and password are required.");

			bool isValid = await _userDetailsService.AuthenticateUser(login.UserName, login.UserPassword);

			if (!isValid)
				return Unauthorized("Invalid username or password.");

			return Ok("Authentication successful.");
		}

		/// <summary>
		/// Gets user session information
		/// </summary>
		/// <param name="login">The login credentials</param>
		/// <returns>The user session information</returns>
		/// <response code="200">Returns the user session</response>
		/// <response code="400">If credentials are missing</response>
		/// <response code="401">If credentials are invalid</response>
		[HttpPost("session")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status400BadRequest)]
		[ProducesResponseType(StatusCodes.Status401Unauthorized)]
		public async Task<ActionResult<SessionModel>> GetUserSession([FromBody] LoginDto login)
		{
			if (string.IsNullOrEmpty(login.UserName) || string.IsNullOrEmpty(login.UserPassword))
				return BadRequest("Username and password are required.");

			var session = await _userDetailsService.GetUserSession(login.UserName, login.UserPassword);

			if (session?.userDetail == null)
				return Unauthorized("Invalid credentials.");

			return Ok(session);
		}
	}
}
