using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TiacPraksaP1.DTOs.Login;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Services.Interfaces;

namespace TiacPraksaP1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public IdentityController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        /// <summary>
        /// This method checks if the username exists, then if it does it checks if the password hashes match, if all of these pass the user gets a token 
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest model)
        {
            var user = await _userManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            if (!await _userManager.CheckPasswordAsync(user, model.Password))
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id)
            };
            authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

            var token = GetToken(authClaims);

            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                expiration = token.ValidTo,
                roles = userRoles
            });
        }

        /// <summary>
        /// This method generates a jwt that lasts 1 hour
        /// </summary>
        /// <param name="authClaims"></param>
        /// <returns></returns>
        private JwtSecurityToken GetToken(List<Claim> authClaims)
        {
            var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]!));

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: authClaims,
                signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

        /// <summary>
        /// This method registers a user if his password is valid
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        [Route("register-user")]
        public async Task<IActionResult> RegisterUser([FromBody] UserPostRequest model, string role="User")
        {
            return await Register(model, role);
        }
        /// <summary>
        /// This method registers a user if his password is valid
        /// </summary>
        /// <param name="model"></param>
        /// <param name="role"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        [Route("register-admin")]
        //[Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterAdmin([FromBody] UserPostRequest model, string role = "Admin")
        {
            return await Register(model, role);
        }

        private async Task<IActionResult> Register([FromBody] UserPostRequest model, string role)
        {
            var existingUser = await _userManager.FindByNameAsync(model.UserName);
            if (existingUser != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "User already exists!" });

            var user = new User
            {
                Email = model.Email,
                UserName = model.UserName,
            };

            var createUserResult = await _userManager.CreateAsync(user, model.Password);
            if (!createUserResult.Succeeded)
                return BadRequest(new { message = "User creation failed! Please check user details and try again.", errors = createUserResult.Errors });

            var roleExists = await _roleManager.RoleExistsAsync(role);
            if (!roleExists)
            {
                var createRoleResult = await _roleManager.CreateAsync(new IdentityRole { Name = role });
                if (!createRoleResult.Succeeded)
                    return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Role creation failed!", errors = createRoleResult.Errors });
            }

            var addToRoleResult = await _userManager.AddToRoleAsync(user, role);
            if (!addToRoleResult.Succeeded)
                return StatusCode(StatusCodes.Status500InternalServerError, new { message = "Failed to add user to role!", errors = addToRoleResult.Errors });

            return Ok(new { message = "User created successfully!" });
        }


    }
}
