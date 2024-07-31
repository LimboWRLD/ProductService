using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Services.Service;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly UserValidator _userValidator;

        public UsersController(UserValidator userValidator, IUserService userService)
        {
            _userValidator = userValidator;
            _userService = userService;
        }
        /// <summary>
        /// This method returns all the users from the database
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetResponse>>> GetUsers()
        {
            var response = await _userService.GetAllUsers();
            if (response.IsNullOrEmpty())
            {
                return NotFound("No users were found");
            }
            return Ok(response);
        }
        /// <summary>
        /// This method returns a user from the database that has the passed id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserGetResponse>> GetUser(string id)
        {
            var response = await _userService.GetSpecificUser(id);
            if (response == null)
            {
                return NotFound("User was not found");
            }
            return Ok(response);
        }
        /// <summary>
        /// This method updates a user if a user with the passed id exists
        /// </summary>
        /// <param name="userPostRequest"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<ActionResult<UserPostResponse>> UpdateUser([FromBody] UserPostRequest userPostRequest, [FromRoute]string id)
        {
            var result = _userValidator.Validate(userPostRequest);
            if (result.IsValid)
            {
                var response = await _userService.UpdateUser(id,userPostRequest);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest("User was not updated!");
            }
            return BadRequest("User was not valid!");
        }
        /// <summary>
        /// This method adds a user to the database if it is valid
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<UserPostResponse>> AddUser([FromBody] UserPostRequest request)
        {
            var result = _userValidator.Validate(request);
            if (result.IsValid)
            {
                var response = await _userService.CreateUser(request);
                if (response == null)
                {
                    return BadRequest("User was not added.");
                }
                return Ok(response);
            }
            return BadRequest("User was not added because user fields were not valid.");
        }
        /// <summary>
        /// This method deletes a user with that id from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult<UserDeleteResponse>> DeleteUser([FromRoute]string id)
        {
            var response = await _userService.DeleteUser(id);
            if (response == null)
            {
                return NotFound("User was not found");
            }
            return Ok(response);
        }
    }
}
