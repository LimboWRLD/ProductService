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
        /// <summary>
        /// This method sends first validates the post request sent, if valid the userProduct is added
        /// </summary>
        /// <param name="userProductPostRequest"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method returns all the userProducts in the database
        /// </summary>
        /// <returns></returns>
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
        /// <summary>
        /// This method returns a specific userProduct from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// This method updates the specific userProduct with the passed productId, if the request is valid
        /// </summary>
        /// <param name="userPostRequest"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPut("{productId:int}")]
        public async Task<ActionResult<UserPostResponse>> UpdateUserProduct([FromBody]UserProductPostRequest userPostRequest, [FromRoute] int productId)
        {
            var result = _userProductValidator.Validate(_mapper.Map<UserProduct>(userPostRequest));
            if(result.IsValid)
            {
                var response = await _userProductService.UpdateUserProduct(productId, userPostRequest);
                if(response == null) 
                {
                    return BadRequest("Updating went wrong...");
                }
                return Ok(response);
            }
            return BadRequest("UserProduct was not valid " + result.ToString());
        }
        /// <summary>
        /// This method deletes the passed userProduct from the database that has the passed productId and userId 
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
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
