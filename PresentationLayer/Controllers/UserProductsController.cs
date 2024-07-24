using AutoMapper;
using BusinessLogicLayer.DTOs.Get;
using BusinessLogicLayer.DTOs.Post;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Validators;
using DataAccessLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using TiacPraksaP1.DTOs.Delete;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Services.Interfaces;

namespace PresentationLayer.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserProductsController : ControllerBase
    {
        private readonly IUserProductService _userProductService;

        private readonly UserProductValidator _userProductValidator;

        private readonly IMapper _mapper;
        public UserProductsController(IUserProductService userService, UserProductValidator userProductValidator, IMapper mapper)
        {
            _userProductService = userService;
            _userProductValidator = userProductValidator;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ActionResult<ProductPostResponse>> AddUserProduct([FromBody] UserProductPostRequest userProductPostRequest)
        {
            var result = _userProductValidator.Validate(_mapper.Map<UserProduct>(userProductPostRequest));
            if (result.IsValid)
            {
                var response = await _userProductService.CreateUserProduct(userProductPostRequest);
                if (response != null)
                {
                    return Ok(response);
                }
                return BadRequest("Adding went wrong");
            }
            return BadRequest("UserProduct fields were not valid " + result.ToString());
        }

        [HttpGet]
        public async Task<ActionResult<List<UserProductGetResponse>>> GetAllUserProducts()
        {
            var response = await _userProductService.GetAllUserProducts();
            if (response == null)
            {
                return BadRequest("No products were found");
            }
            return Ok(response);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserProductGetResponse>>GetUserProduct(int id)
        {
            var response = await _userProductService.GetSpecificUserProduct(id);    
            if(response == null)
            {
                return NotFound("No userProduct was found");
            }
            return Ok(response);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<UserPostResponse>> UpdateUserProduct([FromBody]UserProductPostRequest userPostRequest, [FromRoute] int id)
        {
            var result = _userProductValidator.Validate(_mapper.Map<UserProduct>(userPostRequest));
            if(result.IsValid)
            {
                var response = await _userProductService.UpdateUserProduct(id, userPostRequest);
                if(response == null) 
                {
                    return BadRequest("Updating went wrong...");
                }
                return Ok(response);
            }
            return BadRequest("UserProduct was not valid " + result.ToString());
        }

        [HttpDelete]
        public async Task<ActionResult<ProductDeleteResponse>> DeleteUserProduct(int productId)
        {
            var response = await _userProductService.DeleteUserProduct(productId);
            if(response != null)
            {
                return Ok(response);
            }
            return BadRequest("UserProduct was not deleted, there is no user product with that id.");
        }
    }
}
