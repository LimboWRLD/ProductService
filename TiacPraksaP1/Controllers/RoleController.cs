using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.IdentityModel.Tokens;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Get;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    public class RoleController: ControllerBase
    {
        private readonly IRoleService _roleService;

        private readonly RoleValidator _validator;

        public RoleController(IRoleService roleService, RoleValidator validator)
        {
            _roleService = roleService;
            _validator = validator;
        }
        [HttpPost]
        public async Task<ActionResult<RolePostResponse>> AddRole(RolePostRequest rolePostRequest) {
            var result  = _validator.Validate(rolePostRequest);
            if(result.IsValid) 
            {
                var response =await _roleService.CreateRole(rolePostRequest);
                if(response != null)
                {
                    return Ok(result);
                }
                return BadRequest("Something went wrong");
            }
            throw new ArgumentException("Role post request was not valid! "+ result.ToString());
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RoleGetResponse>>> GetAllRoles()
        {
            var response  = await _roleService.GetAllRoles();
            if (response.IsNullOrEmpty())
            {
                return NotFound("No roles were found!");
            }
            return Ok(response);
        }

        [HttpGet("{int:id}")]
        public async Task<ActionResult<RoleGetResponse>> GetRole(int id) {
            var response =await _roleService.GetSpecificRole(id);
            if( response != null)
            {
                return Ok(response);
            }
            return NotFound("No role was found");
        }

        [HttpPut]
        public async Task<ActionResult<RolePostResponse>>UpdateRole(RolePostRequest rolePostRequest)
        {
            var result = _validator.Validate(rolePostRequest);
            if(result.IsValid)
            {
                var response =await _roleService.UpdateRole(rolePostRequest);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest("Role was not updated!");
            }
            throw new ArgumentException("Role was not valid! " + result.ToString());
        }

        [HttpDelete]
        public async Task<ActionResult<UserDeleteResponse>>DeleteRole(int id) {
            var response =await _roleService.DeleteRole(id);
            if(response != null)
            {
                return Ok("Role was deleted. " + response);
            }
            return NotFound("Something went wrong, role was not found. "); 
        }
    }
}
