using AutoMapper;
using BusinessLogicLayer.DTOs.Post;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TiacPraksaP1.DTOs.Post;
using TiacPraksaP1.Models;
using TiacPraksaP1.Services.Interfaces;
using TiacPraksaP1.Validators;

namespace TiacPraksaP1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUserProductService _userProductService;
        private readonly ProductValidator _productValidator;
        private readonly IMapper _mapper;

        public ProductsController(IProductService productService, ProductValidator productValidator, IMapper mapper, IUserProductService userProductService)
        {
            _productService = productService;
            _productValidator = productValidator;
            _mapper = mapper;
            _userProductService = userProductService;
        }
        /// <summary>
        /// This method first verifies that the product is valid according to the product validator class, if it is valid it is created 
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        //Create
        [HttpPost]
        public async Task<ActionResult<ProductPostResponse>> AddProduct([FromBody]ProductPostRequest product)
        {
            var result = _productValidator.Validate(_mapper.Map<Product>(product));
            if(result.IsValid)
            {
                var response = await _productService.CreateProduct(product);
                 
                if (response == null)
                {
                    return BadRequest("Adding went wrong.");
                }
                await _userProductService.CreateUserProduct(new UserProductPostRequest { ProductId = response.Id });
                return Ok(response);
            }
            return BadRequest("Product fields were not valid" + result.ToString());
        }
        /// <summary>
        /// This method gets all the products that the user is assosiated with.
        /// </summary>
        /// <returns></returns>
        //Get 
        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            var response =await _productService.GetAllProducts();
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }
        /// <summary>
        /// This method returns a dictionary with the basic statistic info of the product table
        /// </summary>
        /// <returns></returns>
        [HttpGet("Staticstics")]
        public async Task<ActionResult<Dictionary<string, string>>> GetBasicStatistics()
        {
            var response = await _productService.GetBasicStatistics();
            if (response == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(response);
        }
        /// <summary>
        /// This method returns a list of most popular products, if range is defined it returns with the defined range, if not it returns the default range
        /// </summary>
        /// <param name="range"></param>
        /// <returns></returns>
        [HttpGet("Staticstics/MostPopular")]
        public async Task<ActionResult<Dictionary<string, string>>> GetMostPopular(int? range)
        {
            var response = await _productService.GetMostPopular(range);
            if (response == null)
            {
                return BadRequest("Something went wrong");
            }
            return Ok(response);
        }

        /// <summary>
        /// This method returns a product if that product is assinged to the user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var response =await _productService.GetSpecificProduct(id);
            if (response == null)
            {
                return NotFound("No products were found");
            }
            return Ok(response);
        }
        /// <summary>
        /// This method updates a product if it exists, only admin users can edit products
        /// </summary>
        /// <param name="id"></param>
        /// <param name="UpdatedProduct"></param>
        /// <returns></returns>
        //Update
        [HttpPut("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<ProductPostResponse>>> UpdateProduct([FromRoute]int id,[FromBody] ProductPostRequest UpdatedProduct)
        {
            var result = _productValidator.Validate(_mapper.Map<Product>(UpdatedProduct));

            if (result.IsValid)
            {
                var response =await _productService.UpdateProduct(id,UpdatedProduct);
                if (response == null)
                {
                    return NotFound("No products were found");
                }
                return Ok(response);
            }
            return BadRequest("Product was not valid" + result.ToString());
        }
        

        /// <summary>
        /// This method deletes a product if it exists, only admin users can delete
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        //Delete
        [HttpDelete("{id:int}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Product>>> DeleteProduct(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if(response != null)
            {
                return Ok(response);
            }
            return NotFound("Product was not deleted. There was no product found.");
        }
    }
}
