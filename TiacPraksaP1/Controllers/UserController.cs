using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly UserValidator _userValidator;

        public UserController(UserValidator userValidator, IUserService userService)
        {
            _userValidator = userValidator;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserGetResponse>>> GetUsers()
        {
            var response =await _userService.GetAllUsers();
            if(response.IsNullOrEmpty())
            {
                return NotFound("No users were found");
            }
            return Ok(response);
        }

        [HttpGet("{int:id}")]
        public async Task<ActionResult<UserGetResponse>>GetUser(int id)
        {
            var response =await _userService.GetSpecificUser(id);
            if(response == null)
            {
                return NotFound("User was not found");
            }
            return Ok(response);    
        }

        [HttpPost]
        public async Task<ActionResult<UserPostResponse>>AddUser(UserPostRequest request)
        {
            var result  = _userValidator.Validate(request);
            if(result.IsValid)
            {
                var response =await  _userService.CreateUser(request);
                if(response == null)
                {
                    return BadRequest("User was not added.");
                }
                return Ok(response);
            }
            throw new ArgumentException("User fields were not valid! " + result.ToString());
        }

        [HttpDelete]
        public async Task<ActionResult<UserDeleteResponse>> DeleteUser(int id) 
        {
            var response =await _userService.DeleteUser(id); 
            if (response == null)
            {
                return NotFound("User was not found");
            }
            return Ok(response);
        }
    }
}
